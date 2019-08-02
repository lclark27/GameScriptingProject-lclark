using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats myStats;

    private GameObject BattleManager;

    public enum EnemyTypes
    {
        small,
        medium,
        large
    }

    public EnemyTypes myType;

    void Start()
    {
        //Find our BattleManager
        BattleManager = GameObject.FindGameObjectWithTag("BattleManager");
        myStats = GetComponent<Stats>();
        switch (myType)
        {
            case EnemyTypes.small:
                break;
            case EnemyTypes.medium:
                break;
            case EnemyTypes.large:
                break;
        }
        //StartCoroutine(randomDelay());
    }

    public void Defeated()
    {
        BattleManager.GetComponent<BattleManager>().RemoveEnemy(gameObject);
    }
}
