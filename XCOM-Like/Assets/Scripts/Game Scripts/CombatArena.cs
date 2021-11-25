using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library.Graph;
using Library.Algorithms;

public class CombatArena : Map
{
    protected int numRows, numCols;
    protected Transform attackSpawn, defSpawn;

    protected Tile emptyTile = default, coverTile = default, concealTile = default;

    public GameObject emptyTileGO, coverTileGO, concealTileGO;

    protected Tile[,] board;

    protected Graph<Tile> tileGraph;
    protected Graph<Tile> preferredAttach;

    public override void init()
    {

        roomType = RoomType.COMBAT_ARENA;
        tileGraph = new Graph<Tile>();
        preferredAttach = new Graph<Tile>();
        tileGraph.allowSelfConnect = true;
        preferredAttach.allowSelfConnect = true;

        emptyTile = emptyTileGO.GetComponent<Tile>();
        coverTile = coverTileGO.GetComponent<Tile>();
        concealTile = concealTileGO.GetComponent<Tile>();

        board = new Tile[boardSize.y, boardSize.x];

        setupGraphPrefferedAttach();

        initBoardPreferredAttach();
    }

    /// <summary>
    /// Initialises board using the Preferred Attachment Algorithm
    /// </summary>
    /// <param name="size">Dimension of of the board</param>
    private void initBoardPreferredAttach()
    {
        PreferredAttachment<Tile> preferredAttachmentAlgo = new PreferredAttachment<Tile>(preferredAttach);
        preferredAttachmentAlgo.setCostModifier(0.75);

        Queue<GameObject> gameObjects = new Queue<GameObject>();


        for (int row = 0; row < boardSize.y; row++)
        {
            if (row == 0 || row == boardSize.y - 1)
            {
                gameObjects.Enqueue(emptyTileGO);
            }
            else
            {
                //Tile tile = preferredAttachmentAlgo.get
            }
        }
        print(gameObjects.Count);
        spawnTiles(gameObjects);

        

        print(preferredAttach.getSize());
    }

    /// <summary>
    /// Enqueues elements from <paramref name="newQueue"/> into <paramref name="sourceQueue"/>
    /// </summary>
    /// <param name="sourceQueue"></param>
    /// <param name="newQueue"></param>
    /// <returns><paramref name="sourceQueue"/> with all elements from <paramref name="newQueue"/> </returns>
    Queue<GameObject> queueAddRange(Queue<GameObject> sourceQueue, Queue<GameObject> newQueue)
    {
        foreach(GameObject gameObject in newQueue)
        {
            sourceQueue.Enqueue(gameObject);
        }

        return sourceQueue;
    }

    /// <summary>
    /// Creates preferred attachment graph
    /// </summary>
    private void setupGraphPrefferedAttach()
    {
        if (emptyTile == null || coverTile == null || concealTile == null)
        {
            print("Reinitialising --- One of the Tile Prefabs failed");
            init();
        }

        preferredAttach.insert(this.emptyTile);
        preferredAttach.insert(this.coverTile);
        preferredAttach.insert(this.concealTile);

        preferredAttach.connectNodes(this.emptyTile, this.emptyTile, 0.5);
        preferredAttach.connectNodes(this.emptyTile, this.coverTile, 0.2);
        preferredAttach.connectNodes(this.emptyTile, this.concealTile, 0.3);

        preferredAttach.connectNodes(this.concealTile, this.concealTile, 0.3);
        preferredAttach.connectNodes(this.concealTile, this.emptyTile, 0.3);
        preferredAttach.connectNodes(this.concealTile, this.coverTile, 0.4);

        preferredAttach.connectNodes(this.coverTile, this.coverTile, 0.2);
        preferredAttach.connectNodes(this.coverTile, this.concealTile, 0.5);
        preferredAttach.connectNodes(this.coverTile, this.emptyTile, 0.3);
    }

    /// <summary>
    /// Uses a queue of tiles to generate a board of tiles
    /// Can be overriden in child classes thanks to "virtual"
    /// </summary>
    /// <param name="tileQueue">Queue of tiles</param>
    protected override void spawnTiles(Queue<GameObject> tileQueue)
    {

        print($"{tileQueue.Count}");

        for (int row = 0; row < boardSize.y; row++)
        {
            for (int col = 0; col < boardSize.x; col++)
            {

                GameObject tileGO = tileQueue.Dequeue();
                Tile tile = tileGO.GetComponent<Tile>();
                board[row,col] = tile;
                tileMap.SetTile(new Vector3Int(row,col,0), tile.tileBase);
            }
        }
    }

    protected Queue<GameObject> convertTileQueue(Queue<Tile> tiles)
    {
        Queue<GameObject> gameObjects = new Queue<GameObject>();
        
        foreach (Tile tile in tiles)
        {
            GameObject go = getPairedPrefab(tile);

            gameObjects.Enqueue(go);
        }
        


        return gameObjects;
    }

    /// <summary>
    /// Graph<T> needs T to implement IComparable
    /// This method takes <paramref name="tile"/> and returns emptyTileGO, coverTileGO ...
    /// This method will be deprecated if Graph no longer needs to implement IComparable
    /// </summary>
    /// <param name="tile"></param>
    /// <returns>The associated prefab and null if not found</returns>
    protected GameObject getPairedPrefab(Tile tile)
    {
        if (tile is EmpyTile)
            return emptyTileGO;
        if (tile is CoverTile)
            return coverTileGO;
        if (tile is ConcealmentTile)
            return concealTileGO;

        print("NULL");

        return null;
    }

    private void OnValidate()
    {
        if (boardSize.x < 2)
        {
            boardSize.x = 2;
        }

        if (boardSize.y < 2)
        {
            boardSize.y = 2;
        }
    }
}
