using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Stats myStats;
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

    public EnemyTypes myType;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        myStats.health = 45;
    }
    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.health -= incDmg - myStats.defense;
        myStats.myStatus = incEffect;
    }
    public void AttackTarget()
    {
        Attacked(myStats.attack, Stats.StatusEffect.none);
    }
}
