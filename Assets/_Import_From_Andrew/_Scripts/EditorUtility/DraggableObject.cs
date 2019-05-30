using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startingPosition;
    private Vector3 startingObjectPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            startingPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            startingPosition.z = 0;
            startingObjectPosition = transform.position;
    #if UNITY_EDITOR
            Selection.activeGameObject = gameObject;
    #endif
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            GameObject newObject = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
    #if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(newObject, "Created New object : " + newObject.name);
    #endif
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        newPosition.z = 0;
        transform.position = startingObjectPosition + (newPosition - startingPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}