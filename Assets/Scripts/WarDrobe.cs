using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarDrobe : MonoBehaviour
{
    public int quality;

    [SerializeField]
    public UnityEvent<int> onChanging;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void FreshChange()
    {
        onChanging.Invoke(quality);
    }
}
