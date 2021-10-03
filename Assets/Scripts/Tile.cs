using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int X { get => x; }
    public int Y { get => y; }

    public Item item;

    private int x;
    private int y;

    public void Init(int x, int y, Item item)
    {
        this.x = x;
        this.y = y;
        SetItem(item);
    }

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void RemoveItem()
    {
        this.item = null;
    }





}
