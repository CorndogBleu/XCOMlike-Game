using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Neighbour
{
    EMPTY,
    COVER,
    CONCEAL
}

public class Tile : MonoBehaviour
{
    public Material baseMaterial;
    public Material secondaryMaterial;
    public GameObject prefab;

    MeshRenderer renderer = default;

    bool walkable;

    /// <summary>
    /// Used for preferred attachment algorithm
    /// </summary>
    public double costToEmptyCell, costToCoverCell, costToConcealCell;

    // Start is called before the first frame update
    void Start()
    {
        baseMaterial = GetComponent<Material>();
        renderer = GetComponent<MeshRenderer>();

        Mathf.Clamp((float)costToConcealCell, 0.0f, 1.0f);
        Mathf.Clamp((float)costToCoverCell, 0.0f, 1.0f);
        Mathf.Clamp((float)costToEmptyCell, 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer == null)
        {
            print("null rendered");
            renderer = GetComponent<MeshRenderer>();
        }
    }

    /// <summary>
    /// Handler for when the mouse hovers over a tile
    /// </summary>
    protected void OnMouseOver()
    {
        if (renderer == null)
        {
            renderer = GetComponent<MeshRenderer>();
        }
        else
        {
             renderer.material = secondaryMaterial;
        }
        
    }


    /// <summary>
    /// Handler for when the mouse no longer hovers over a tile
    /// </summary>
    protected void OnMouseExit()
    {
        //print("Exit");
        renderer.material = baseMaterial;
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
}
