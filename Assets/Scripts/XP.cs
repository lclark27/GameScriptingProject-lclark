using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{

    //establishing variables for script
    public int hp;
    public int attack;
    public int defence;
    public int speed;
    public int skill;
    public int luck;

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

        //increase will be determined by a 1-20 dice roll with 1 being 100% and 20 being 5%. If value of roll is over percentage chance then stat goes up.
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

        //level up initiation
        level += 1;
        experience = 0;
        hp += 100;

        //increase experience required cap
        experiencerequired += 100;
        Debug.Log("Level up!");

        if (level > 1) //checking player level
        {
            Statup();
        }
        else
        {

        }
 

        void Statup() //stat up determination system
        {
            //attack level up
            if (Random.Range(1, 21) > 10)
            {
                attack += 1;
                Debug.Log("ATK Up!");
            }
            else
            {
                attack += 0;
            }
            //defence level up
            if (Random.Range(1, 21) > 12)
            {
                defence += 1;
                Debug.Log("DEF Up!");
            }
            else
            {
                defence += 0;
            }
            //speed level up
            if (Random.Range(1, 21) > 11)
            {
                speed += 1;
                Debug.Log("SPD Up!");
            }
            else
            {
                speed += 0;
            }
            //skill level up
            if (Random.Range(1, 21) > 10)
            {
                skill += 1;
                Debug.Log("SKILL Up!");
            }
            else
            {
                skill += 0;
            }
            //luck level up
            if (Random.Range(1, 21) > 13)
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
