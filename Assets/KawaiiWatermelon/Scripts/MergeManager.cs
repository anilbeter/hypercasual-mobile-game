using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fruit.onCollisionWithFruit += MergeFruitsCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MergeFruitsCallback(Fruit sender)
    {
        Debug.Log("Collision detected by " + sender.name);
        // Collision detected by Fruit_02(Clone)
    }
}
