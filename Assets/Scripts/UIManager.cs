using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class UIManager : MonoBehaviour
{

    public ItemAmount itemAmountPrefab;
    public List<ItemAmount> itemAmounts;

    private void Start()
    {
        Grid.instance.OnGridChange += UpdateItemAmount;
        itemAmountPrefab.gameObject.SetActive(false);
    }

    public void UpdateItemAmount()
    {
        var groupedTiles = Grid.instance.tiles
            .Where(x => x.item != null)
            .GroupBy(x => x.item.settings.id)
            .OrderByDescending(x => x.Count())
            .ToList();


        foreach (var group in groupedTiles)
        {
            var settings = group.ElementAt(0).item.settings;

            if (!itemAmounts.Any(i => i.id == settings.id))
            {
                var newLine = Instantiate(itemAmountPrefab, itemAmountPrefab.transform.parent);
                newLine.Init(Resources.Load<Sprite>(settings.spritePath), settings.id, settings.title);
                itemAmounts.Add(newLine);
            }

            var text = itemAmounts.First(i => i.id == settings.id);
            text.UpdateAmount(group.Count());
            text.transform.SetSiblingIndex(groupedTiles.IndexOf(group));

        }

        foreach (var textLint in itemAmounts)
        {
            if (!groupedTiles.Any(x => x.Key == textLint.id))
            {
                textLint.UpdateAmount(0);
            }


        }

    }




}
