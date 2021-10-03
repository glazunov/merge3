using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class CombinationChecker
{
    private Grid grid;
    private Tile tile;
    private List<List<Tile>> xAndYLinesCointainsTile = new List<List<Tile>>();

    private List<CombinationResult> results = new List<CombinationResult>();

    public Grid Grid { get => grid; private set => grid = value; }
    public Tile Tile { get => tile; private set => tile = value; }
    public List<List<Tile>> xAndYlLinesCointainsTile { get => xAndYLinesCointainsTile; private set => xAndYLinesCointainsTile = value; }
    public List<CombinationResult> Results { get => results; private set => results = value; }

    public CombinationChecker(Grid grid, Tile tile)
    {
        this.Grid = grid;
        this.Tile = tile;

        var collumn = grid.tiles.Where(t => t.X == tile.X).OrderBy(t => t.Y).ToList();
        var line = grid.tiles.Where(t => t.Y == tile.Y).OrderBy(t => t.Y).ToList();


        xAndYlLinesCointainsTile.Add(collumn);
        xAndYlLinesCointainsTile.Add(line);

        Results.AddRange(GetWinCombinations(tile, collumn));
        Results.AddRange(GetWinCombinations(tile, line));

    }

    public List<CombinationResult> GetWinCombinations(Tile t, List<Tile> line)
    {
        var indexOfTile = line.IndexOf(t);


        Tile[] tilesToCheck =
        {
                indexOfTile - 4 >= 0 ?  line[indexOfTile - 4] : null,
                indexOfTile - 3 >= 0 ? line[indexOfTile - 3] : null,
                indexOfTile - 2 >= 0 ?  line[indexOfTile - 2] : null,
                indexOfTile - 1 >= 0 ? line[indexOfTile - 1] : null,
                t,
                indexOfTile + 1 < line.Count ? line[indexOfTile + 1]: null,
                indexOfTile + 2 < line.Count ? line[indexOfTile + 2] : null,
                indexOfTile + 3 < line.Count ? line[indexOfTile + 3]: null,
                indexOfTile + 4 < line.Count ? line[indexOfTile + 4] : null
            };

        return new CombinationResult[] {
                new CombinationResult(t, tilesToCheck.ToList().Skip(0).Take(5).ToList()),  //checking xxxxX
                new CombinationResult(t, tilesToCheck.ToList().Skip(1).Take(5).ToList()),  //checking xxxXx
                new CombinationResult(t, tilesToCheck.ToList().Skip(2).Take(5).ToList()),  //checking xxXxx
                new CombinationResult(t, tilesToCheck.ToList().Skip(3).Take(5).ToList()),  //checking xXxxx
                new CombinationResult(t, tilesToCheck.ToList().Skip(4).Take(5).ToList()),  //checking Xxxxx

                new CombinationResult(t, tilesToCheck.ToList().Skip(2).Take(3).ToList()), //checking xxX
                new CombinationResult(t, tilesToCheck.ToList().Skip(3).Take(3).ToList()), //checking xXx
                new CombinationResult(t, tilesToCheck.ToList().Skip(4).Take(3).ToList())  //checking Xxx            
           }.ToList();
    }
}
