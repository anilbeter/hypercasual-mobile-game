using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fruit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FruitType fruitType;

    [Header("Actions")]
    public static Action<Fruit> onCollisionWithFruit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void MoveTo(Vector2 position)
    {
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.GetFruitType() != fruitType)
                return;

            else
                onCollisionWithFruit?.Invoke(this);
        }
    }

    public FruitType GetFruitType()
    {
        return fruitType;
    }

}
