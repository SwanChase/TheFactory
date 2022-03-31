using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DommeCodeVanYouriHoi : MonoBehaviour
{
    [SerializeField] private Material knipClosed;
    [SerializeField] private Material knipOpen;
    [SerializeField] private float changeTime;
    private bool schaaarClosed;

    private void OnMouseDown()
    {
        StartCoroutine("MaterialChange");
    }
    private void OnMouseUp()
    {
        StopCoroutine("MaterialChange");
        GetComponent<MeshRenderer>().material = knipClosed;
        schaaarClosed = true;
    }
    IEnumerator MaterialChange() 
    {
        yield return new WaitForSeconds(changeTime);
        if (schaaarClosed) 
        {
            GetComponent<MeshRenderer>().material = knipOpen;
            schaaarClosed = false;
        }
        else 
        {
            GetComponent<MeshRenderer>().material = knipClosed;
            schaaarClosed = true;
        }
        StartCoroutine("MaterialChange");
    }

}
