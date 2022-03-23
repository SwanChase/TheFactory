using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarDrobe : MonoBehaviour
{
    public int quality;

    public GameObject playerskin;

    Renderer skinrend;

    public Material newOutfit, oldOutfit;

    [SerializeField]
    public UnityEvent<int> onChanging;

    // Start is called before the first frame update
    void Start()
    {
        skinrend = playerskin.GetComponent<MeshRenderer>();

        if (onChanging == null)
        {
            onChanging = new UnityEvent<int>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetQuality(int newQuality)
    {
        quality = newQuality;
        skinrend.material = newOutfit;
    }

    public void FreshChange()
    {
        onChanging.Invoke(quality);
    }

    public void lowQuality()
    {
        skinrend.material = oldOutfit;
    }
}
