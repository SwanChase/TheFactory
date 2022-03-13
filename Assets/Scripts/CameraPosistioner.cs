using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosistioner : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform TestObject;

    private void Start()
    {
        cam = cam.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveCameraTo(TestObject);
        }
    }
    public void MoveCameraTo(Transform objPos)
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, objPos.position,Time.deltaTime);
    }
}