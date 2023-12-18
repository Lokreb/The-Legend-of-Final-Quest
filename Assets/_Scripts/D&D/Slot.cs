using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool IsOccupied { get; private set; } = false;
    public static bool pointerIsOnSlot = false;

    public void OnDrop(PointerEventData eventData)
    {
        Drag draggableObject = eventData.pointerDrag.GetComponent<Drag>();

        if (draggableObject != null)
        {
            draggableObject.RectTransform.SetParent(transform);
            draggableObject.RectTransform.anchoredPosition = Vector2.zero;
            IsOccupied = true;
            draggableObject.OnEndDrag(eventData);
        }
    }

    public void ClearSlot()
    {
        IsOccupied = false;
    }
}