using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorDragAndDrop : MonoBehaviour
{
    [SerializeField] private Camera scissorCam;
    //private Vector3 mOffset;
    //private float mZCoord;

    private bool isDraggingDeez = false;


    private void OnMouseDown()
    {
        isDraggingDeez = true;
        //mZCoord = scissorCam.WorldToScreenPoint(gameObject.transform.position).z;
        //mOffset = gameObject.transform.position - GetMouseWorldPos();
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

    //private Vector3 GetMouseWorldPos()
    //{
    //    Vector3 mousePoint = Input.mousePosition;
    //    mousePoint.z = mZCoord;
    //    return Camera.current.ScreenToWorldPoint(mousePoint);
    //}

    //private void OnMouseDrag()
    //{
    //    transform.position = GetMouseWorldPos() + mOffset;
    //}
}
