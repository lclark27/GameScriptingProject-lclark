using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    private Button attackButton;
    private Button defendButton;
    private Button healButton;

    private Image pHealthBarFill;
    private Image eHealthBarFill;


    // Start is called before the first frame update
    void Start()
    {
        //UpdateHealthBar(true, amount);
        BManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        System.Action.UpdateHealthBar += BattleManager.UpdateHealth();
    }

    public void UpdateHealthBar(bool isPlayer, float health)
    {
        //we will handle fill amount back in the respective scripts calling this function
        if(isPlayer)
        {
            pHealthBarFill.fillAmount = health;
        }
        else
        {
            eHealthBarFill.fillAmount = health;

        }
    }

}
