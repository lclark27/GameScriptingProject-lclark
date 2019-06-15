using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    public int hp = 100;
    public int attack = 0;
    public int defence = 0;
    public int speed = 0;
    public int skill = 0;
    public int luck = 0;

    public int level;
    public int experience;
    public int experiencerequired;


    // Start is called before the first frame update
    void Start()
    {
        //starting values, declaring starting stats and percentage chances of skill ups
        level = 1;
        hp = 100;
        attack = 0;
        defence = 0;
        speed = 0;
        skill = 0;
        luck = 0;
        experience = 0;
        experiencerequired = 100;
        print("Current Level = 1");
        print("Percentage Chance of stats going up:");

        //increase will be determined by a 1-20 dice roll with 1 being 5% and 20 being 100%. If value of roll is over percentage chance then stat goes up.
        print("HP = 100%");
        print("Attack = 50%");
        print("Defence = 40%");
        print("Speed = 55%");
        print("Skill = 50%");
        print("Luck = 35%");
    }

    // Update is called once per frame
    void Update()
    {

        //trigger to gain exp
        Exp();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            experience += 50;
            Debug.Log("Experience Gained.");
            Debug.Log("Current XP is " + experience + "" );
        }
    }

    void Exp()
    {
        //trigger to initiate level up
        if(experience >= experiencerequired)
            Levelup();
    }

    void Levelup()
    {

        //level up
        level += 1;
        experience = 0;
        hp += 100;
        experiencerequired += 100;
        Debug.Log("Level up!");

        if (level > 1) //need formule to check if a level up occured)
        {
            Statup();
        }
        else
        {

        }
 

        void Statup()
        {
            //attack level up
            if (Random.Range(1, 21) > 9)
            {
                attack += 1;
                Debug.Log("ATK Up!");
            }
            else
            {
                attack += 0;
            }
            //defence level up
            if (Random.Range(1, 21) > 7)
            {
                defence += 1;
                Debug.Log("DEF Up!");
            }
            else
            {
                defence += 0;
            }
            //speed level up
            if (Random.Range(1, 21) > 10)
            {
                speed += 1;
                Debug.Log("SPD Up!");
            }
            else
            {
                speed += 0;
            }
            //skill level up
            if (Random.Range(1, 21) > 9)
            {
                skill += 1;
                Debug.Log("SKILL Up!");
            }
            else
            {
                skill += 0;
            }
            //luck level up
            if (Random.Range(1, 21) > 6)
            {
                luck += 1;
                Debug.Log("LUCK Up!");
            }
            else
            {
                luck += 0;
            }
        }


    }
}
