using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarDrobe : MonoBehaviour
{
    public int quality;
    public int durability;

    [SerializeField]
    public UnityEvent<int,int> onChanging;

    // Start is called before the first frame update
    void Start()
    {
        if (onChanging == null)
        {
            onChanging = new UnityEvent<int, int>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FreshChange()
    {
        onChanging.Invoke(quality, durability);
        
    }
    public void SetQuality(int newQuality)
    {
        quality = newQuality;
    }
    public void SetDurability(int newDurability)
    {
        durability = newDurability;
    }
}
