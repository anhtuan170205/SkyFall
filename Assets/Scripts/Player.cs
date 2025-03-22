using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 mousePosition;

    private void Update()
    {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        int clampedX = Mathf.Clamp(Mathf.RoundToInt(mousePosition.x), -2, 2);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}
