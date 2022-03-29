using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    public void startBGM ()
    {
        MyEvent.Post(gameObject);
    }
}