using System;
using System.Collections.Generic;
using UnityEngine;

public class FruitFactory : PooledFactory<Fruit>
{
    private readonly HashSet<Fruit> _createdObjects;
    private readonly AllFruits _allFruits;
    
    private FruitType _currentFruitType;

    public FruitFactory(AllFruits allFruits)
    {
        _allFruits = allFruits;
        _createdObjects = new HashSet<Fruit>();
    }

    public Fruit GetOrCreate(FruitType type, Transform parent, Vector3 position)
    {
        _currentFruitType = type;
        Fruit obj = Get(cube => cube.FruitType == type);
        obj.transform.SetParent(parent);
        obj.transform.position = position;

        _createdObjects.Add(obj);
        return obj;
    }

    public void ReleaseAll()
    {
        foreach (Fruit cube in _createdObjects)
        {
            cube.ReleaseToPool();
        }
            
        _createdObjects.Clear();
    }

    protected override Fruit Create()
    {
        Fruit prefab = _allFruits.GetFruit(_currentFruitType);
        Fruit fruit = GameObject.Instantiate(prefab);
        _createdObjects.Add(fruit);
        return fruit;
    }

    protected override Fruit Get(Func<Fruit, bool> predicate)
    {
        Fruit fruit = base.Get(predicate);
        fruit.Show();
        fruit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        return fruit;
    }

    protected override void Release(Fruit fruit)
    {
        base.Release(fruit);
        fruit.Hide();
    }
}