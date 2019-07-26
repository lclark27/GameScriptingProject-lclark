using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{

    public int playerLives = 3;

    public int attendanceRate;
    public int attending = 8;
    public int classSize = 12;

    public bool classCounted;


    void Start()
    {
        //HelloWorld();
        ModifyLives(-2);
        DisplayName("Jake", "Thomas");
        attendanceRate = Mathf.RoundToInt((float)attending / (float)classSize * 100);
        
        Debug.Log("Attendance %: " + attendanceRate);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //very simple coin toss
            //1 is winner
            
            //if(Random.Range(successCalc,100) > 75)

            if(Random.Range(1,7) > 4)
            {
                HelloWorld();
                ModifyLives(2);
                Debug.Log("Won the coin toss");
            }
            else
            {
                Debug.Log("Lost the coin toss");
                ModifyLives(-2);
            }
        }

        if(!classCounted)
        {
            for(int i = 1; i < classSize; i++)
            {
                Debug.Log(i);
            }
            classCounted = true;
        }
    }

    void HelloWorld()
    {
        Debug.Log("Hello World");
    }

    void ModifyLives(int incLives)
    {
        playerLives += incLives;
        //Debug.Log(incLives);
    }

    void DisplayName(string firstName, string lastName)
    {
        Debug.Log(firstName + " " + lastName);
    }



}

