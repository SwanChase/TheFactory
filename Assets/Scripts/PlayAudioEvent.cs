using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    public void startEvent()
    {
        MyEvent.Post(gameObject);
    }
}