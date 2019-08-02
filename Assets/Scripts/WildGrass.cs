using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGrass : MonoBehaviour
{
    private GameObject gameManager;

    public bool isInGrass;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        RollDice();
    }

    void RollDice()
    {
        int diceRoll = Random.Range(1, 7);
        Debug.Log(diceRoll);
        if (diceRoll == 3 && isInGrass)
        {
            //Encounter triggered. Load the battle scene
            
            gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.BattleScene);
        }
        StartCoroutine(CheckTimer());
    }


    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isInGrass = true;
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isInGrass = false;
        }
    }


    IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(2);
        RollDice();
    }
}
