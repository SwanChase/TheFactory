using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour
{

    public void onClick()
    {
        AkSoundEngine.PostEvent("UI_Click_Option", gameObject);
    }

}
