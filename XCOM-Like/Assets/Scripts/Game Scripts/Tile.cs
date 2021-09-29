using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material baseMaterial;
    public Material secondaryMaterial;

    MeshRenderer renderer = default;
    

    bool walkable;

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
