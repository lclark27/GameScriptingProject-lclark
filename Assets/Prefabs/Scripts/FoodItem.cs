using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public enum FoodTypes
    {
        Eggs,
        Bacon,
        Fish,
        Ham,
        Toast,
        Sausages,
        Apples,
        Cereal,
        Porridge,
        Soup,
        Spaghetti
    }

    public FoodTypes myType;
}
