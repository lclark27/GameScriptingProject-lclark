using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerV2 : MonoBehaviour
{
    //just like an enemy list, this is where all of our different prefabs will go

    public List<GameObject> buffet;

    //assume that i want to battle a whole team of enemies, and we will juist fight them ojne at a time
    //until athey are all defeated.
    public List<GameObject> plate;

    public GameObject foodToEat;

    //how many items, do we want on our plate at once? | how many enemies on the team to defeat?
    public int numOfFoodItemsOnPlate;

    // Start is called before the first frame update
    void Start()
    {
        //Select the enemies to fight out of the giantr list of dirrernet enemy types
        for(int i = 0; i < numOfFoodItemsOnPlate; i++)
        {
            //add a random food item from the buffet to our plate, remember they are already different types
            GameObject spawnedFood = Instantiate(buffet[Random.Range(0, buffet.Count)]);
            plate.Add(spawnedFood);
        }
    }
void SetNewFoodToEat()
    {
        //Randomly get something from palte, assign it to foodtoEat
        foodToEat = plate[Random.Range(0, plate.Count)];
        StartCoroutine(Eating());
    }

    void EatFood()
    {
        plate.Remove(foodToEat);
        Destroy(foodToEat);
        //check if we have eaten all the food, if we havent eat something else
        if(plate.Count == 0)
        {
            //if its empty we win
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
