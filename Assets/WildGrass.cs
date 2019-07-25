using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGrass : MonoBehaviour
{
    public bool isInGrass;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        RollDice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RollDice()
    {
        if (Random.Range(0,6)==3)
        {
            //function to encounter an enemy
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
        if (col.gameObject.tag == "Player")
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
