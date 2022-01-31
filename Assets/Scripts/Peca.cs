using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Peca : MonoBehaviour
{
    private bool isDragging = false;
    private RectTransform rectTransform;
    private Vector3[] pecaPositions = new Vector3[9];
    private EventTrigger eventTrigger;

    private void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void ClickDown()
    {
        if (!isDragging)
        {
            if (!QuebraCabeca.ganhou)
                rectTransform.Rotate(0f, 0f, 90f);
            print("mouse down");
        }
    }

    public void ClickDrag()
    {
        isDragging = true;
        print("mouse drag");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePos);
        rectTransform.position = objPosition;
    }

    public void EndDrag()
    {
        isDragging = false;
    }

    public void EndOfTime()
    {
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = event
        //eventTrigger.triggers.Add()
    }
}
