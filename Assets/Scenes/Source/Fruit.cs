using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum FruitType
{
    Apple = 0,
    Banana = 1,
    Orange = 2,
    Pear = 3,
    Strawberry = 4,
    Starfruit = 5,
    Coco = 6,
    Cherry = 7,
}

[RequireComponent(typeof(MeshCollider), typeof(Rigidbody))]
public class Fruit : MonoBehaviour, IPoolable<Fruit>
{
    [field: SerializeField] public FruitType FruitType { get; set; }
    public Action<Fruit> Release { get; set; }

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(HideFruit());
    }

    private IEnumerator HideFruit()
    {
        yield return new WaitForSeconds(5f);
        ReleaseToPool();
    }

    private void FixedUpdate()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ReleaseToPool()
    {
        Release?.Invoke(this);
    }
}