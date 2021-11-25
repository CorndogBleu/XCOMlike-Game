using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Map : MonoBehaviour
{
    public enum RoomType
    {
        NOT_INTIALISED,
        EMPTY_ROOM,
        STORE,
        COMBAT_ARENA
    }

    protected RoomType roomType;
    public Tilemap tileMap;
    public List<TileBase> tileBases;
    public Vector2Int boardSize;

    // Start is called before the first frame update
    void Start()
    {
        roomType = RoomType.NOT_INTIALISED;

        print(GameObject.FindGameObjectsWithTag("TileMap").Length);

        if (tileMap == null)
        {
            tileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tileMap == null)
        {
            tileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
        }
    }

    public RoomType getRoomType()
    {
        return roomType;
    }

    abstract public void init();
    abstract protected void spawnTiles(Queue<GameObject> queue);

    
}
