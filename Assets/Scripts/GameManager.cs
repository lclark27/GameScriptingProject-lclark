using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> EnemySpawnList;
    public List<GameObject> EnemiesToFight;

    public enum Worlds
    {
        Overworld,
        BattleScene
    }

    private static GameManager gameManRef;

    //void Awake is called before void Start on ANY OBJECT
    void Awake()
    {
        if (gameManRef == null)
        {
            gameManRef = this;
            //This will make it so we can travel between scenes (good for keeping track of gameplay!)
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        //we start in the overworld so lets load the shizzle
        LoadPlayerStuff(true);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.Overworld:
                //load overworld scene
                SavePlayerStuff(false);
                SceneManager.LoadScene("Overworld");
                LoadPlayerStuff(true);
                break;
            case Worlds.BattleScene:
                //load battle scene
                GenerateEnemies();
                SavePlayerStuff(true);
                SceneManager.LoadScene("BattleScene");
                LoadPlayerStuff(false);
                break;
        }
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            //add random enemies to fight from our list, this will run each time we enter wild grass!
            EnemiesToFight.Add(EnemySpawnList[Random.Range(0, EnemySpawnList.Count)]);
        }
    }

    void SavePlayerStuff(bool isFromOverworld)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //only save position in overworld
        if (isFromOverworld)
        {
            //save both location and rotation as seperate floats, using similar naming conventions
            PlayerPrefs.SetFloat("playerPosx", playerObj.transform.position.x);
            PlayerPrefs.SetFloat("playerPosy", playerObj.transform.position.y);
            PlayerPrefs.SetFloat("playerPosz", playerObj.transform.position.z);
            PlayerPrefs.SetFloat("playerRotx", playerObj.transform.rotation.x);
            PlayerPrefs.SetFloat("playerRoty", playerObj.transform.rotation.y);
            PlayerPrefs.SetFloat("playerRotz", playerObj.transform.rotation.z);
        }

        //Save stats that we need to track!
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        PlayerPrefs.SetFloat("playerHealth", playerStats.health);
        PlayerPrefs.SetInt("playerCurrentExp", playerStats.curExp);
        PlayerPrefs.SetInt("playerLevel", playerStats.level);
    }

    void LoadPlayerStuff(bool goingToOverworld)
    {
        //load the existing stats and apply them to the player!
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerStats.health = PlayerPrefs.GetFloat("playerHealth", playerStats.maxHealth);
        playerStats.curExp = PlayerPrefs.GetInt("playerCurrentExp", 0);
        playerStats.level = PlayerPrefs.GetInt("playerLevel", 1);
        //load position only in overworld
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (goingToOverworld)
        {
            playerObj.transform.position = new Vector3(PlayerPrefs.GetFloat("playerPosx", 0f),
                                                       PlayerPrefs.GetFloat("playerPosy", 2f),
                                                       PlayerPrefs.GetFloat("playerPosz", -8f));

            playerObj.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("playerRotx", 0f),
                                                       PlayerPrefs.GetFloat("playerRoty", 0f),
                                                       PlayerPrefs.GetFloat("playerRotz", 0f));
        }

    }

    public void DeleteSavedStuff()
    {
        //hard reset
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Overworld");
    }

}