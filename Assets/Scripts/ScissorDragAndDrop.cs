using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorDragAndDrop : MonoBehaviour
{
    [SerializeField] private Camera scissorCam;
    private bool isDraggingDeez = false;


    private void OnMouseDown()
    {
        isDraggingDeez = true;
    }

    private void OnMouseUp()
    {
        isDraggingDeez = false;
    }

    private void Update()
    {
        if (isDraggingDeez)
        {
            Vector2 mousPos = scissorCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousPos);
        }
    }
}
