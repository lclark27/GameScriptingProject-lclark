using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    private Button attackButton;
    private Button defendButton;
    private Button healButton;

    public Image pHealthBarFill;
    public Image eHealthBarFill;

    public BattleManager bManager;

    //we use System. to avoid Random.Range errors (and other syntax errors)
    public event System.Action CallAttack;
    public event System.Action CallDefend;
    public event System.Action CallHeal;

    public Text[] combatLogLines;
    public List<string> combatLog;

    //void Awake runs before void Start (on any script that is created at the same time as it)
    void Awake()
    {
        //UpdateHealthBar(true, amount);
        bManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        //original event on left, subscriber on the right.
        bManager.UpdateHealth += UpdateHealthBar;
    }

    private void Start()
    {
        StartCoroutine(DebugLogTest());
    }

    public void UpdateHealthBar(bool isPlayer, float health)
    {
        //we will handle fill amount back in the respective scripts calling this function.
        if (isPlayer)
        {
            pHealthBarFill.fillAmount = health;
        }
        else
        {
            eHealthBarFill.fillAmount = health;
        }
    }

    //Since we can't call events directly from the Buttons in the UI we're just going
    //to make these functions
    public void CallAttackEvent()
    {
        Debug.Log("Attack");
        CallAttack();
    }
    public void CallDefendEvent()
    {
        Debug.Log("Defend");
        CallDefend();
    }
    public void CallHealEvent()
    {
        Debug.Log("Heal");
        CallHeal();
    }

    public void UpdateCombatLog(string incLog)
    {
        //add string to list (at position [0]! super important!)
        combatLog.Insert(0, incLog);
        //if list length is > array length, delete the last entry [last index]
        if (combatLog.Count > combatLogLines.Length)
        {
            combatLog.RemoveAt(combatLog.Count - 1);
        }
        //loop through array and set the text to the strings.
        for (int i = 0; i < combatLog.Count; i++)
        {
            combatLogLines[i].text = combatLog[i];
        }
        StartCoroutine(DebugLogTest());
    }

    IEnumerator DebugLogTest()
    {
        int randomNumber = Random.Range(1, 1000);
        yield return new WaitForSeconds(1f);
        UpdateCombatLog("Your random number is: " + randomNumber.ToString());
    }

}