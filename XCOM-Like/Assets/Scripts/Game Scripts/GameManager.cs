using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum BattleSate
    {
        PLAYER_TURN,
        AI_TURN,
        PLAYER_WIN,
        PLAYER_LOST
    }

    [SerializeField]
    Map map = default;

    int momentum;
    BattleSate battleSate;

    public LayerMask tileLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        try
        {
            map.init();
        }
        catch(UnassignedReferenceException e)
        {
            map.tileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<UnityEngine.Tilemaps.Tilemap>();
            map.init();
        }
    }

    public int getMomentum()
    {
        return momentum;
    }
    public BattleSate getBattleSate()
    {
        return battleSate;
    }

}
