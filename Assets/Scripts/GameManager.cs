using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Worlds
    {
        Overworld,
        BattleScene
    }

    //void Awake is called before void Start on ANY OBJECT
    void Awake()
    {
        //Allows travel between scenes to keep track of gameplay
        DontDestroyOnLoad(this.gameObject);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.Overworld:
                //load overworld scene
                SceneManager.LoadScene("Overworld");
                break;
            case Worlds.BattleScene:
                //load battle scene
                SceneManager.LoadScene("BattleScene");
                break;
        }
    }
}
