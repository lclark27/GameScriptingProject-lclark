using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{

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
    private GameObject battleUIManager;
    private bool doBattle = true;

    //events only need types, not variable names
    public event System.Action<bool, float> UpdateHealth;

    private void Awake()
    {
        //Sub to BattleUIManager
        battleUIManager = GameObject.FindGameObjectWithTag("BattleUIManager");
        battleUIManager.GetComponent<BattleUIManager>().CallAttack += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallDefend += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallHeal += CheckCombatState;
        //You would need to probably have an enum called "playerDecision" which would keep track
        //of whatever button was pressed (decision made) and then call CheckCombatState using that
        //on the players turn. Automatically run during the enemies turn but turn it back to manual
        //during the players turn (you can use coroutines and bools to handle this!)
    }

    // Start is called before the first frame update
    void Start()
    {
        //find our game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //copy list of enemies to spawn from
        foreach(GameObject tempEnemy in gameManager.GetComponent<GameManager>().EnemiesToFight)
        {
            enemySpawnList.Add(tempEnemy);
        }
        //clear the list so the gameManager doesn't need to worry about it and we're ready to go for next time!
        gameManager.GetComponent<GameManager>().EnemiesToFight.Clear();
        //Spawn our first enemy
        SpawnEnemy();
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

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        //delete enemy from scene (maybe do cool particles or explosion or something)
        Destroy(enemyToRemove);
        //make sure to update our spawn list to get rid of the defeated enemy! (usually first position)
        enemySpawnList.RemoveAt(0);
    }

    public void SpawnEnemy()
    {
        if(enemySpawnList.Count > 0)
        {
            //get the spawn location for the enemies using a tag like "EnemySpawnLoc"
            Transform EnemySpawnLoc = GameObject.FindGameObjectWithTag("EnemySpawnLoc").transform;
            //assign the enemy obj
            enemyObj = Instantiate(enemySpawnList[0], EnemySpawnLoc);
        }
        else
        {
            combatState = CombatState.Victory;
        }
        
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
                {
                    RemoveEnemy(enemyObj);
                    SpawnEnemy();
                }   
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
        //Setup temporary float value for fill amount (0.0f - 1.0f) by simply dividing current health by max health
        float percentage = defender.GetComponent<Stats>().health / defender.GetComponent<Stats>().maxHealth;
        //debug for reasons
        Debug.Log(percentage);
        //Since the first variable is a bool we can use this like it's an if statement
        UpdateHealth(combatState == CombatState.PlayerTurn, percentage);
    }

    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }

}
