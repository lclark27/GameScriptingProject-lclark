using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerV2 : MonoBehaviour
{
    //just like an enemy list, this is where all of our different prefabs will go
    public List<GameObject> buffet;

    //Assume that I want to battle a whole team of enemies, and we will just fight them one at a time
    //Until they are all defeated! Obv we might only fight 3 out of the 11 diff types. So we need a new list!
    public List<GameObject> plate;

    //foodToEat ~ EnemyToFight
    public GameObject foodToEat;

    //how many items, do we want on our plate at once? | How many enemies on the team to defeat?
    public int numOfFoodItemsOnPlate;

    // Start is called before the first frame update
    void Start()
    {
        //Select the enemies to fight out of the giant list of different enemy types
        for(int i = 0; i < numOfFoodItemsOnPlate; i++)
        {
            //add a random food item from the buffet to our plate, remember they are already different types!
            GameObject spawnedFood = Instantiate(buffet[Random.Range(0, buffet.Count)], transform);
            plate.Add(spawnedFood);
        }
        //Actually start setting which food to eat
        SetNewFoodToEat();
    }
    
    void SetNewFoodToEat()
    {
        //Randomly get something from our plate, assign it to foodToEat
        foodToEat = plate[Random.Range(0,plate.Count)];
        StartCoroutine(Eating());
    }

    void EatFood()
    {
        //remove the current food we are eating
        plate.Remove(foodToEat);
        Destroy(foodToEat);
        //Check if we have eaten all the food! If we haven't, eat something else.
        if(plate.Count == 0)
        {
            //If we have, we win!
            Debug.Log("You ate all the food! Congrats!");
        }
        else
        {
            //Eat something else
            SetNewFoodToEat();
        }
    }

    IEnumerator Eating()
    {
        yield return new WaitForSeconds(5);
        EatFood();       
    }
}
