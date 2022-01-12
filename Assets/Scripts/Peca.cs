using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca : MonoBehaviour
{
    private bool isDragging = false;
    public void ClickDown()
    {
        if (!isDragging)
        {
            if (!QuebraCabeca.ganhou)
                transform.Rotate(0f, 0f, 90f);
            print("mouse down");
        }
    }

    public void ClickDrag()
    {
        isDragging = true;
        print("mouse drag");
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = objPosition;
    }

    public void EndDrag()
    {
        isDragging = false;
    }
}
