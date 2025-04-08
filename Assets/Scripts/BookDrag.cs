using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDrag : MonoBehaviour
{
    public Camera dragCamera; // Cámara específica que usaremos para convertir el mouse a posición del mundo

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    void Update()
    {
        if (isBeingHeld && dragCamera != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = dragCamera.ScreenToWorldPoint(mousePos);

            this.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && dragCamera != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = dragCamera.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
