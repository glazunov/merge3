using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    [Range(0, 32)]
    public int startItemsAmount = 10;

    [Range(0, 10)]
    public int limitItemsTypesOnStart = 3;


    public List<Tile> tiles;
    public Item itemPrefab;

    public UnityAction OnGridChange;

    void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        InitTilesOnStart();

        tiles
            .OrderBy(t => Guid.NewGuid())
            .Take(startItemsAmount)
            .ToList()
            .ForEach(x =>
            {
                AddItem(ItemManager.instance.itemDatas[UnityEngine.Random.Range(0, limitItemsTypesOnStart)].id, x);
            });

        yield return new WaitForEndOfFrame();

        OnGridChange?.Invoke();
    }

    public void InitTilesOnStart()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].Init(i % 8, i / 8);
        }
    }



    public void OnItemDropOnTile(Item item, Transform tileTransform)
    {
        //tiles.ForEach(x => x.GetComponent<SpriteRenderer>().color = Color.white);

        var tile = tiles.FirstOrDefault(x => x.transform == tileTransform);
        if (tile == null)
        {
            item.MoveToCurrentTile();
            return;
        }

        if (tile.item != null)
        {
            item.MoveToCurrentTile();
        }
        else
        {
            item.ChangeTile(tile);
            item.MoveToCurrentTile();
            StartCoroutine(CheckForWinCombinations(tile, 0.3f)); //todo magic
        }


    }

    IEnumerator CheckForWinCombinations(Tile tile, float delay = 0)
    {
        CombinationChecker checker = new CombinationChecker(this, tile);

        if (checker.Results.Any(x => x.Win))
        {
            var winCombination = checker.Results
                .Where(x => x.Win)
                .GroupBy(x => x.SameItemsAmount)
                .OrderByDescending(x => x.Key)
                .First()
                .OrderBy(x => Guid.NewGuid())
                .First();

            var rewardId = winCombination.Item.settings.rewardId;
            winCombination.Combination.ForEach(t => t.item.MoveToTile(tile));

            yield return new WaitForSeconds(0.3f);

            winCombination.Combination.ForEach(t =>
            {
                Destroy(t.item.gameObject);
                t.item = null;
            });

            AddItem(rewardId, tile, true);
            if (winCombination.SameItemsAmount == 5)
            {
                AddItem(rewardId, GetNearestEmptyTile(tile), true, tile);
            }



        }
        yield return new WaitForEndOfFrame();
        OnGridChange?.Invoke();



    }

    public Tile GetNearestEmptyTile(Tile from)
    {
        var fromVector = new Vector2(from.X, from.Y);

        return tiles
            .Where(x => x.item == null)
            .GroupBy(x => Vector2.Distance(new Vector2(x.X, x.Y), fromVector))
            .OrderBy(x => x.Key)
            .First()
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefault();
    }

    public void AddItem(string id, Tile t, bool shake = false, Tile moveFromTile = null)
    {
        var item = Instantiate(itemPrefab);
        var data = ItemManager.instance.itemDatas.FirstOrDefault(x => x.id == id);
        if (data == null)
            Debug.LogError(id);
        item.Init(data, t);
        if (moveFromTile != null)
            item.transform.position = moveFromTile.transform.position + Vector3.up * Item.yOffsetFromTile;
        else
            item.transform.position = t.transform.position + Vector3.up * Item.yOffsetFromTile;

        item.MoveToCurrentTile();
        if (shake)
            item.PlayShakeAnimation(0.3f);
    }

}





