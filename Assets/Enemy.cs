using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats myStats;
    //enemy 0 = small
    //enemy 1 = medium
    //enemy 2 = large

    public int enemyID = 1;

    public enum EnemyTypes
    {
        small,
        medium,
        large,
    }
    private GameObject gameManager;
    public EnemyTypes myType;

    // Start is called before the first frame update
    void Start()
    {
        //Find our gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        myStats = GetComponent<Stats>();
        switch (myType)
            {
            case EnemyTypes.small:
                //do setup
                break;
            case EnemyTypes.medium:
                break;
            case EnemyTypes.large:
                break;
            }
    }
    /*
    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.health -= incDmg - myStats.defense;
        myStats.myStatus = incEffect;
        if (myStats.health <= 0)
            myStats.isDefeated = true;
    }

    /*
    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Player>().Attacked(myStats.attack, Stats.StatusEffect.none);
    }
    */


    public void Defeated()
    {
        gameManager.GetComponent<GameManager>().RemoveEnemy(gameObject);
    }

}
