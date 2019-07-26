using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{

    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;

    public enum GameState
    {
        notInCombat,
        InCombat
    }
    public GameState gameState;

    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Loss
    }
    public CombatState combatState;
    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;
    private GameObject gameManager;
    private bool doBattle = true;

    public event System.Action<bool, float> UpdateHealth;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(enemy);
        }
        //find our game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        
    }

    void Update()
    {
        if(doBattle)
        {
            //Set turn based on playerObj speed and enemyObj speed
            //Fastest should go first, random if same
            StartCoroutine(battleGo());
            doBattle = false;
        }
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

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
    }

    public void SpawnEnemy()
    {
        //Spawn an enemy from our list of spawnable enemies
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
                BattleRound(playerObj, enemyObj);
                //Check if Enemy is defeated
                if (enemyObj.GetComponent<Stats>().isDefeated)
                    SpawnEnemy();
                //Next Case. Most likely EnemyTurn
                combatState = CombatState.EnemyTurn;
                break;
            //Enemy Turn
            case CombatState.EnemyTurn:
                //Decision - Attack
                //Attack the player
                BattleRound(enemyObj, playerObj);
                //Check if Player is defeated
                if(playerObj.GetComponent<Stats>().isDefeated)
                {
                    //set state to lose cause we died
                    combatState = CombatState.Loss;
                    Debug.Log("Lose");
                    break;
                }
                //Next Case. Most likely PlayerTurn
                combatState = CombatState.PlayerTurn;
                break;
            //Victory
            case CombatState.Victory:
                Debug.Log("You are win");
                //You will want to assign exp here, before you travel, otherwise it could be an issue where you don't get your reward!
                //Travels out to the overworld!
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
                break;
            //Tell the player they won
            //End Game
            case CombatState.Loss:
                //we lose, reset game
                //Loss
                //Tell the player they lost
                //Restart Game
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
                break;
        }

    }

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //will take an attacker and defender and make them do combat
        //you will need to add a chance of them missing based on one of their stats
        //something like evasion/accuracy
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().attack, Stats.StatusEffect.none);
        Debug.Log(attacker.name +
            " attacks " +
            defender.name +
            " for a total of " +
            (attacker.GetComponent<Stats>().attack - defender.GetComponent<Stats>().defense) + 
            " damage");
    }

    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }

}
