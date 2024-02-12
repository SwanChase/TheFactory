using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayBGM : MonoBehaviour
{
    static bool bGPlaying = false;
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    void Start()
    {
        if (bGPlaying == false)
        {
            MyEvent.Post(gameObject);
            bGPlaying = true;
        }
    }
}