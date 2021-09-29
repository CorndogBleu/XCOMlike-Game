using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    

    // Start is called before the first frame update
    void Start()
    {
        roomType = RoomType.NOT_INTIALISED;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RoomType getRoomType()
    {
        return roomType;
    }

    abstract public void init(Vector2Int boardSize);
    
}
