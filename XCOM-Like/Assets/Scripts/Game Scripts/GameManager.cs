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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        map.init(new Vector2Int(11, 11));
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
