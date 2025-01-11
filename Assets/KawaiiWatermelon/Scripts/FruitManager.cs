using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;

    [Header("Settings")]
    [SerializeField] private float fruitsYSpawnPosition;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ManagePlayerInput();
    }

    private void ManagePlayerInput()
    {
        Vector2 spawnPosition = GetClickedWorldPosition();
        spawnPosition.y = fruitsYSpawnPosition;

        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

#if UNITY_EDITOR
    // Show the line where the fruits will spawn
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-50, fruitsYSpawnPosition, 0), new Vector3(50, fruitsYSpawnPosition, 0));
    }
#endif
}
