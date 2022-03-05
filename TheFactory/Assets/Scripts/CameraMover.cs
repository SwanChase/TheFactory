using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] Rigidbody rBody;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float speedZoom;
    [SerializeField] float minCameraSize, maxCameraSize;
    IEnumerator cameraMove = null;

    Vector3 initialPos;

    void Start()
    {
        initialPos = Camera.main.transform.position;
    }
    void Update()
    {
        moveControl();
        zoomControl();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
        {
            if (cameraMove != null)
            {
                StopCoroutine(cameraMove);
                cameraMove = null;
            }
        }
    }

    private void moveControl()
    {
        if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.RightShift)))
        {
            acceleration = 100;
        }
        else
        {
            acceleration = 10;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rBody.AddForce(new Vector3(-acceleration, 0, acceleration));
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rBody.AddForce(new Vector3(acceleration, 0, -acceleration));
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rBody.AddForce(new Vector3(acceleration, 0, acceleration));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rBody.AddForce(new Vector3(-acceleration, 0, -acceleration));
        }

        if (rBody.velocity.x > maxSpeed)
        {
            rBody.velocity = new Vector3(maxSpeed, rBody.velocity.y, rBody.velocity.z);
        }
        if (rBody.velocity.y > maxSpeed)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, maxSpeed);
        }
    }

    private void zoomControl()
    {
        if (Camera.main.orthographicSize - Input.mouseScrollDelta.y >= minCameraSize && Camera.main.orthographicSize - Input.mouseScrollDelta.y <= maxCameraSize)
        {
            Camera.main.orthographicSize -= Input.mouseScrollDelta.y * speedZoom;
        }
    }

    public void moveCamera()
    {
        if (cameraMove == null)
        {
            cameraMove = moveCameraToInitialPoint();
            StartCoroutine(cameraMove);
        }
    }

    IEnumerator moveCameraToInitialPoint()
    {
        Vector3 temp = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
        while (Vector3.Distance(initialPos, temp) > 5)
        {
            temp = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
            transform.position = temp;
            yield return new WaitForEndOfFrame();
        }
        //transform.Translate(initialPos);
        cameraMove = null;
    }
}
