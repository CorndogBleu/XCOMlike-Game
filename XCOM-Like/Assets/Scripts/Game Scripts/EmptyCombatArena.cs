using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library.Graph;

public class EmptyCombatArena : CombatArena
{

    public override void init()
    {
        roomType = RoomType.COMBAT_ARENA;
        tileGraph = new Graph<Tile>();
        tileGraph.allowSelfConnect = true;

        emptyTile = emptyTileGO.GetComponent<Tile>();

        board = new Tile[boardSize.x, boardSize.y];

        Queue<Tile> tiles = new Queue<Tile>();


        for(int x = 0; x < boardSize.x; x++)
        {
            for(int y = 0; y < boardSize.y; y++)
            {
                tiles.Enqueue(emptyTile);
            }
        }
        
        spawnTiles(convertTileQueue(tiles));
    }


    
}
