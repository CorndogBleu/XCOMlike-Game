using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //TODO drag and drop
    //TODO snap to tile

    Transform parent;
    Vector3 prevPos;
    bool dragging;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        dragging = false;
        prevPos = transform.position;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseDrag()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
    //    Vector3 mouseCoord = ray.GetPoint(dist);
    //    mouseCoord.y = 1.5f;
    //    transform.position = mouseCoord;
    //    dragging = true;
    //}

    //private void OnMouseUp()
    //{
    //    if (dragging)
    //    {
    //        snapToTile();
    //        dragging = false;
    //    }
    //}

    void snapToTile()
    {
        RaycastHit raycastHit;

        LayerMask layerMask = gameManager.tileLayerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),
            out raycastHit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastHit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }

        //transform.SetParent(raycastHit.transform);
    }
}
