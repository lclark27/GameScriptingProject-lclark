using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Stats myStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.health -= incDmg - myStats.defense;
        myStats.myStatus = incEffect;
        if (myStats.health <= 0)
            myStats.isDefeated = true;
    }
    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Enemy>().Attacked(myStats.attack, Stats.StatusEffect.none);
    }

}
