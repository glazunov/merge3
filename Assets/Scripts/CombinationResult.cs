using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class CombinationResult
{
    private Tile tile;
    private Item item;
    private List<Tile> combination;
    private int sameItemsAmount;
    private bool win;

    public Tile Tile { get => tile; private set => tile = value; }
    public Item Item { get => item; private set => item = value; }
    public List<Tile> Combination { get => combination; private set => combination = value; }
    public int SameItemsAmount { get => sameItemsAmount; private set => sameItemsAmount = value; }
    public bool Win { get => win; private set => win = value; }

    public CombinationResult(Tile t, List<Tile> combination)
    {
        this.Tile = t;
        this.Combination = combination;
        this.SameItemsAmount = combination.Count;
        this.Item = t.item;
        this.SameItemsAmount = combination.Count;
        this.Win = CheckForWinCombination(Tile, combination);
    }

    bool CheckForWinCombination(Tile t, List<Tile> tilesToCheck)
    {
        if (tilesToCheck.Contains(null))
            return false;

        if (tilesToCheck.Any(x => x.item == null))
            return false;

        return tilesToCheck.All(x => x.item.settings.id == t.item.settings.id);
    }
}
