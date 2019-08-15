using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int speed;
    public int attack;
    public int defense;
    public int luck;
    public int curExp;
    public int neededExp;
    public int level;

    public bool isDefeated;

    public enum StatusEffect
    {
        none,
        dizzy,
        poisoned,
        stunned
    }

    public StatusEffect myStatus;
    public StatusEffect attackEffect;

    public void Attacked(int incDmg, StatusEffect incEffect)
    {
        health -= incDmg - defense;
        myStatus = incEffect;
        if (health <= 0)
            isDefeated = true;
    }
}
