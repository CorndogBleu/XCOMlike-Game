using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArena : Map
{
    int numRows, numCols;
    Transform attackSpawn, defSpawn;

    [SerializeField]
    Vector2Int boardSize = new Vector2Int (11,11);

    [SerializeField]
    Tile emptyTilePrefab = default, coverTilePrefab = default, concealTilePrefab = default;

    Tile[,] board;

    Graph<Tile> tiles;


    // Start is called before the first frame update
    void Start()
    {
        roomType = RoomType.COMBAT_ARENA;
        tiles = new Graph<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void init(Vector2Int size)
    {
        // initBoardPreferredAttach(size);
    }

    // Initialises board with Preferred Attachment algorithm
    private void initBoardPreferredAttach(Vector2Int size)
    {
        this.boardSize = size;

        board = new Tile[size.x, size.y];

        double rnd = Random.value;
        Tile prevTile;
        int rndInt = (int)(Random.value * 10);

        for (int i = 0; i < 10; i++)
        {
            rndInt = (int)(Random.value * 100);
            print(rndInt);

        }


    }


    private void initOld(Vector2Int size)
    {
        this.boardSize = size;

        board = new Tile[size.x, size.y];
        
        Vector2 offset = new Vector2();
        offset.x = 0.5f * (size.x - 1);
        offset.y = 0.5f * (size.y - 1);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Tile tile = Instantiate(emptyTilePrefab);
                board[x,y] = tile;
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
                
            }
        }

        System.Random rand = new System.Random();

        int numTiles = rand.Next() % ((boardSize.x * boardSize.y) / 4);

        print(numTiles);

        int randX, randY;

        for (int i = 0; i < numTiles; i++)
        {
            randX = rand.Next() % boardSize.x;
            randY = rand.Next() % boardSize.y;
            Tile temp = Instantiate(coverTilePrefab);
            temp.transform.SetParent(transform, false);
            temp.transform.localPosition = board[randX, randY].transform.localPosition;
            Tile delete = board[randX, randY];

            board[randX, randY] = temp;
            Destroy(delete);
            
        }

        numTiles = rand.Next() % ((boardSize.x * boardSize.y) / 4);
        for (int i = 0; i < numTiles; i++)
        {
            randX = rand.Next() % boardSize.x;
            randY = rand.Next() % boardSize.y;

            while (board[randX, randY].GetType() == typeof(CoverTile))
            {
                randX = rand.Next() % boardSize.x;
                randY = rand.Next() % boardSize.y;
                print("already a cover tile");
            }


            Tile temp = Instantiate(concealTilePrefab);
            temp.transform.SetParent(transform, false);
            temp.transform.localPosition = board[randX, randY].transform.localPosition;
            Tile delete = board[randX, randY];

            board[randX, randY] = temp;
            Destroy(delete);

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
