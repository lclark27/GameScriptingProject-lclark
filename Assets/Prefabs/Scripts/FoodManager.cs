using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //imagine all instances of food = enemies

    //prefab that we made that is our various foods
    public GameObject defaultFoodPrefab;

    //list to pick from
    public List<GameObject> buffetItems;
    //list of current items
    public List<GameObject> plateItems;

    //number of items to randomly create
    public int numOfItems;

    // Start is called before the first frame update
    void Start()
    {
        //generate our buffetItems based on how many we set using numOfItems
        for(int i = 0; i < numOfItems; i++)
        {
            //add item to the list
            buffetItems.Add(defaultFoodPrefab);           
        }

        //Select something to put on our plate
        //Taking our list "buffetItems" and getting a random item using Random.Range
        //Random.Range takes two values, a min and max, 0 = start of array so that's the min
        //buffetItems.Count = last index, and since Random.Range is max exclusive, it shouldn't go over max index
        //Should probably use a loop here but I was being lazy
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        //moved here so it only runs once, even if we run AddToPlate multiple times, like above!
        StartCoroutine(Eating());
    }

    void AddToPlate(GameObject foodToAdd)
    {
        //step 1. spawn in the item
        //You can replace "transform" with a set position, perhaps make a spawn location object in the scene!
        GameObject spawnedFood = Instantiate(foodToAdd, transform);
        //step 2. Randomize the type of food it is
        //cast to enum type using (Script.EnumType)
        spawnedFood.GetComponent<FoodItem>().myType = (FoodItem.FoodTypes)Random.Range(0, 11);
        //set the name of the item to be the type of food it is
        spawnedFood.name = spawnedFood.GetComponent<FoodItem>().myType.ToString();
        //step 3. Add item to our plate
        plateItems.Add(spawnedFood);
    }

    //Could also be called "RemoveFromPlate"
    void Eat(GameObject foodToEat)
    {
        //Remove the given item from our plate
        plateItems.Remove(foodToEat);
        //Removes it from the level (cause we ate it!)
        Destroy(foodToEat);
    }

    IEnumerator Eating()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Nom");
        Debug.Log(plateItems[0]);
        //Remove item from our plate (first item)
        Eat(plateItems[0]);
        //Go about adding a new item to the plate from the buffet
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        //put here, so we can do the loop!
        StartCoroutine(Eating());
    }
}
