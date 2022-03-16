using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform TestObject;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            MoveCameraTo(TestObject);
        }*/
    }
    public void MoveCameraTo(Transform objPos)
    {
        StartCoroutine("LerpTo", objPos);
    }

    private IEnumerator LerpTo(Transform endTransform)
    {
        Transform startTransform = cam.transform;

        Vector3 startPos = startTransform.position;
        Vector3 endPos = endTransform.position;

        Quaternion startRot = startTransform.rotation;
        Quaternion endRot = endTransform.rotation;

        float time = 0;

        while (time < 1)
        {
            cam.transform.position = Vector3.Lerp(startPos, endPos, time);
            cam.transform.rotation = Quaternion.Lerp(startRot, endRot, time);
            float deltaTime = Time.deltaTime;
            yield return new WaitForSeconds(deltaTime);
            time += deltaTime;
        }

        cam.transform.position = endPos;
        cam.transform.rotation = endRot;
    }
}