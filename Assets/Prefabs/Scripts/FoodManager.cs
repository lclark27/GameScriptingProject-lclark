using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //prefab that we made that is our various foods
    public GameObject defaultFoodPrefab;

    private List<GameObject> buffetItems;
    //list of current items
    private List<GameObject> plateItems;

    //number of items to randomly create
    public int numOfItems;

    // Start is called before the first frame update
    void Start()
    {
        //generate our buffetItems based on how many we set using numOfItems
        for (int i = 0; i < numOfItems; i++)
        {
            //add item to the list
            buffetItems.Add(defaultFoodPrefab);
//(float)(int / int);
            //cast to enum type using (typeof)
            //randomize the type of food
            buffetItems[i].GetComponent<FoodItem>.myType = (FoodITem.FoodTypes)Random.Range(0, 11);
            //set the name of the item to be the type of food it is
            buffetItems[i].name = buffetItems[i].GetComponent<FoodItems>().myType.ToString();

        }
        //Select something to put on our plate
        //Taking our list "buffetItems" and getting a random item using Random.Range
        //Random.Range takes two values, a min and max, 0 = start of array so that's the min
        //buffetItems.Count = last index, and since Random.Range is max exclusive, it shouldn't go over max index
        AddToPlate(buffetItems[Random.Range(0,buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
    }


    void AddToPlate(GameObject foodToAdd);
    {
         //step 1. spawn in the item
         //You can replace "transform" with a set position, perhaps make a spawn location object in the scene!
         GameObject.spawnedFood = Instantiate(foodtoAdd, transform);

    //step 2 randomize type of food
    //cast to enum type using (EnumType)
    spawnedFood.GetComponent<FoodItem>().MyType = (FoodItem.FoodTypes) Random.Range(0,11);
    //set the name of the item to be the type of food it is
    spawnedFood.name = spawnedFood.GetComponent<FoodItem>().myType.ToString();

    //step 3. add item to our plate
    plateItems.Add(spawnedFood);
    }

void Eat(GameObject foodToEat)
{
    //Remove our item from our list
    plateItems.Remove(foodToEat);
}

IEnumerator Eating()
{
    Debug.Log("Nom");
    Debug.Log(plateItems[0]);
    //Remove item from our plate
    plateItems.Remove(foodtoEat);
    //REmove it from the level
    Destroy(foodtoEat);
    yield return new WaitForSeconds(5);
    //go about adding a new item to the plate from the buffet
    AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
    StartCoroutine(Eating());

}



// Update is called once per frame
void Update()
    {
        
    }
}
