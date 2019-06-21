using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;

    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;

    public enum GameState
    {
        notInCombat,
        InCombat,
    }
    public GameState gameState;

    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Loss,
    }

    public CombatState combatState;

    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;


    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"));
        {
            enemyList.Add(enemy);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health -= 10;
        }
    }

   

public void HealEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health += 10;
        }
                        
    }

public void RemoveEnemy(GameObject enemytoremove)
{
        enemyList.Remove(enemytoremove);
}

public void SpawnEnemy()
{
    //Spaawn an enemy from our list of spawnable enemies
    //using the size of the list as the random range maximum
    Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);
}



public void CheckCombatState()
{
    switch (combatState)
    {
            //Player Turn
            case CombatState.PlayerTurn:

        //Decision - Attack
        //Attack the enemy
        playerObj.GetComponent<Player>().AttackTarget(enemyObj);
        //check if enemy is defeated
        if (enemyObj.GetComponent<Enemy>().myStats.isDefeated)
                SpawnEnemy();
        break;

            //Enemy Turn
            case CombatState.EnemyTurn:
        //decision - attack
        //attack the player
        enemyObj.GetComponent<Enemy>().AttackTarget(playerObj);

        //check if player is defeated
        break;

        //Victory
        //tell the player they won
        //end game

        //Loss

        //tell the player they lost
        //restart game
    }
}
}
