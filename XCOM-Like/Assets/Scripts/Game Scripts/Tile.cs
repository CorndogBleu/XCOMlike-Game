using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public enum Neighbour
{
    EMPTY,
    COVER,
    CONCEAL
}

public class Tile : MonoBehaviour, IComparable
{
    Sprite baseTileSprite;

    [SerializeField]
    Sprite alternateTileSprite;

    SpriteRenderer renderer;

    public TileBase tileBase;

    bool walkable;

    /// <summary>
    /// Used for preferred attachment algorithm
    /// </summary>
    public double costToEmptyCell, costToCoverCell, costToConcealCell;

    // Start is called before the first frame update
    void Start()
    {

        Mathf.Clamp((float)costToConcealCell, 0.0f, 1.0f);
        Mathf.Clamp((float)costToCoverCell, 0.0f, 1.0f);
        Mathf.Clamp((float)costToEmptyCell, 0.0f, 1.0f);

        while (renderer == null)
            renderer = GetComponent<SpriteRenderer>();

        baseTileSprite = renderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Handler for when the mouse hovers over a tile
    /// </summary>
    protected void OnMouseOver()
    {
        renderer.sprite = alternateTileSprite;
    }


    /// <summary>
    /// Handler for when the mouse no longer hovers over a tile
    /// </summary>
    protected void OnMouseExit()
    {
        //print("Exit");
        renderer.sprite = baseTileSprite;
    }

    public bool isWalkable()
    {
        return walkable;
    }

    public float getCostToEmptyCell()
    {
        return (float)costToEmptyCell;
    }

    public float getCostToConcealCell()
    {
        return (float)costToConcealCell;
    }

    public float getCostToCoverTile()
    {
        return (float)costToCoverCell; 
    }

    public void OnValidate()
    {

    }

    public int CompareTo(object obj)
    {
        if (obj == null || !(obj is Tile))
            return 1;

        Tile otherTile = obj as Tile;

        return gameObject.GetInstanceID().CompareTo(otherTile.gameObject.GetInstanceID());
        
    }
}
