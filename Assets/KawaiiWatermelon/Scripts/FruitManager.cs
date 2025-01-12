using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Fruit[] fruitPrefabs;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [Header("Settings")]
    [SerializeField] private float fruitsYSpawnPosition;
    private bool isControlEnabled;
    private bool isControlling;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    void Start()
    {
        isControlEnabled = true;
        HideLine();
    }

    void Update()
    {
        if (isControlEnabled)
            ManagePlayerInput();
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDownCallback();

        // With this logic, the player can hold the mouse button and move the fruit. Even though the player can't see the fruit, it's still being moved. 
        else if (Input.GetMouseButton(0))
        {
            if (isControlling)
                MouseHoldCallback();
            else
                MouseDownCallback();
        }

        else if (Input.GetMouseButtonUp(0) && isControlling)
            MouseUpCallback();
    }

    private void MouseDownCallback()
    {
        ShowLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();

        isControlling = true;
    }

    private void MouseHoldCallback()
    {
        PlaceLineAtClickedPosition();
        currentFruit.MoveTo(GetSpawnPosition());
    }

    private void MouseUpCallback()
    {
        HideLine();
        currentFruit.EnablePhysics();

        isControlEnabled = false;
        StartControlTimer();

        isControlling = false;
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        currentFruit = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], spawnPosition, Quaternion.identity);
    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 clickedWorldPosition = GetClickedWorldPosition();
        clickedWorldPosition.y = fruitsYSpawnPosition;
        return clickedWorldPosition;
    }

    private void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    private void ShowLine()
    {
        fruitSpawnLine.enabled = true;
    }

    private void StartControlTimer()
    {
        Invoke("StopControlTimer", .5f);
    }

    private void StopControlTimer()
    {
        isControlEnabled = true;
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
