using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool doBattle = true;


    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(enemy);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(doBattle)
        {
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
                BattleRound(playerObj, enemyObj);
        //check if enemy is defeated
        if (enemyObj.GetComponent<Stats>().isDefeated)
                SpawnEnemy();

        //next case, most likely enemyturn
                combatState = CombatState.EnemyTurn;
                break;

            //Enemy Turn
            case CombatState.EnemyTurn:
                //decision - attack
                //attack the player
                BattleRound(enemyObj, playerObj);

                //check if player is defeated
                if (playerObj.GetComponent<Stats>().isDefeated)
                {
                    //set state to lose cause we died
                    combatState = CombatState.Loss;
                }
                break;
            //Victory
            //tell the player they won
            //end game

                case CombatState.Victory:
                //we lose, reset game
                Debug.Log("Youre Winner");

                break;

            //Loss
            //tell the player they lost
            //restart game

            case CombatState.Loss:
                //we lose, reset game
                SceneManager.LoadScene("SampleScene");
                break;
        }
}

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //will take an attacker and defender and make them do combat
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().attack, Stats.StatusEffect.none);
        Debug.Log("Attacker: " + attacker.name + " Defender: " + defender.name);
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
