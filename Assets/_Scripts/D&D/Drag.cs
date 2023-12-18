using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform RectTransform { get; private set; }
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (Slot.pointerIsOnSlot)
        {
            // Trouve le component du slot sur lequel on drop
            Slot slot = eventData.pointerEnter.GetComponent<Slot>();

            if (slot != null)
            {
                // Clipse le draggable sur le slot
                RectTransform slotRect = slot.GetComponent<RectTransform>();
                RectTransform.SetParent(slotRect);
                RectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }

    private float GetCanvasScaleFactor()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            return canvas.scaleFactor;
        }
        return 1f;
    }
}