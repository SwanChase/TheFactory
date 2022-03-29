using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootstepSound : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    public void onFoot()
    {
        MyEvent.Post(gameObject);
    }
}