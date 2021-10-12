using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library.Graph;

public class CombatArena : Map
{
    int numRows, numCols;
    Transform attackSpawn, defSpawn;

    [SerializeField]
    Vector2Int boardSize = new Vector2Int (11,11);

    [SerializeField]
    Tile emptyTile = default, coverTile = default, concealTile = default;

    Tile[,] board;

    Graph<Tile> tileGraph;
    Graph<Tile> preferredAttach;
    


    // Start is called before the first frame update
    void Start()
    {
        roomType = RoomType.COMBAT_ARENA;
        tileGraph = new Graph<Tile>();
        preferredAttach = new Graph<Tile>();
        tileGraph.allowSelfConnect = true;
        preferredAttach.allowSelfConnect = true;

        setupGraphPrefferedAttach();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void init(Vector2Int size)
    {

        Start();

        //initBoardPreferredAttach(size);

        initOld(size);
    }

    /// <summary>
    /// Initialises board using the Preferred Attachment Algorithm
    /// </summary>
    /// <param name="size">Dimension of of the board</param>
    private void initBoardPreferredAttach(Vector2Int size)
    {
        this.boardSize = size;
        board = new Tile[boardSize.x, boardSize.y];
        
        Queue<GameObject> tileQueue = new Queue<GameObject>();

        float rnd;

        Tile currTile = emptyTile;

        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                tileQueue.Enqueue(currTile.gameObject);
                rnd = Random.value;

               
            }
        }

        spawnTiles(tileQueue);
        
    }

    /// <summary>
    /// Creates preferred attachment graph
    /// </summary>
    private void setupGraphPrefferedAttach()
    {
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

    private void initOld(Vector2Int size)
    {
        this.boardSize = size;
        board = new Tile[size.x, size.y];
        Queue<GameObject> tiles = new Queue<GameObject>();

        for(int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                if (Random.value < .33)
                {
                    tiles.Enqueue(emptyTile.gameObject);
                }
                else if (Random.value < .66)
                {
                    tiles.Enqueue(coverTile.gameObject);
                }
                else
                {
                    tiles.Enqueue(concealTile.gameObject);
                }
            }
        }

        print($"{tiles.Count}");

        spawnTiles(tiles);
        
    }

    /// <summary>
    /// Uses a queue of tiles to generate a board of tiles
    /// </summary>
    /// <param name="tileQueue">Queue of tiles</param>
    void spawnTiles(Queue<GameObject> tileQueue)
    {
        Vector2 offset = new Vector2();
        offset.x = 0.5f * (boardSize.x - 1);
        offset.y = 0.5f * (boardSize.y - 1);

        if (tileQueue.Count != (boardSize.x * boardSize.y))
        {
            throw new System.Exception("Board dimensions do not match queue size");
        }

        print($"{tileQueue.Count}");

        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                
                GameObject tileGO = Instantiate(tileQueue.Dequeue());
                Tile tile = tileGO.GetComponent<Tile>();
                board[x, y] = tile;
                tileGO.transform.SetParent(transform, false);
                tileGO.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

            }
        }
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
