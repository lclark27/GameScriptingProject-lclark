using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int fruit = 0;
    public int apples = 0;
    public int bananas = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fruit += 1 - bananas;
        apples += 2;
        Debug.Log("There are " + fruit + " fruit");
        Debug.Log("There are " + apples + " apples");

        if(fruit > 7)
        {
            Debug.Log("Greater than 100");
        }
        else
        {
            Debug.Log("Less than 100");
        }
    }
}
