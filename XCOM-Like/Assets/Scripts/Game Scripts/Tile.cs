using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material baseMaterial;
    public Material secondaryMaterial;

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
}
