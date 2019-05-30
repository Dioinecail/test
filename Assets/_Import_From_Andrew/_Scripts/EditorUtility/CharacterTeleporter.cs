using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTeleporter : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, 38);
    private Vector3 mousePos;

    public Transform target;

    void Update()
    {
        GetMousePosition();

        if (Input.GetMouseButtonDown(1) && target != null)
        {
            TeleporCharacter();
        }
    }

    private void GetMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
    }

    private void TeleporCharacter()
    {
        Debug.Log("Teleport");
        Vector3 newPosition = mousePos;
        newPosition.z = target.position.z;

        target.position = newPosition;
    }
}