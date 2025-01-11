using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private GameObject currentFruit;

    [Header("Settings")]
    [SerializeField] private float fruitsYSpawnPosition;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    void Start()
    {
        HideLine();
    }

    void Update()
    {
        ManagePlayerInput();
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDownCallback();
        else if (Input.GetMouseButton(0))
            MouseHoldCallback();
        else if (Input.GetMouseButtonUp(0))
            MouseUpCallback();
    }

    private void MouseDownCallback()
    {
        ShowLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();
    }

    private void MouseHoldCallback()
    {
        PlaceLineAtClickedPosition();
        currentFruit.transform.position = GetSpawnPosition();
    }

    private void MouseUpCallback()
    {
        HideLine();
        currentFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        currentFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
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
