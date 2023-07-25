using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/AllFruits", fileName = "AllFruits")]
public class AllFruits : ScriptableObject, IEnumerable<Fruit>
{
    [SerializeField] private Fruit[] _fruitList;
    public Fruit this[int index] => _fruitList[index];

    public Fruit GetFruit(FruitType type)
    {
        foreach (Fruit food in _fruitList)
        {
            if (food.FruitType == type)
                return food;
        }

        return null;
    }

    public IEnumerator<Fruit> GetEnumerator()
    {
        return ((IEnumerable<Fruit>)_fruitList).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
