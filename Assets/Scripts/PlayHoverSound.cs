using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHoverSound : MonoBehaviour
{

    public void onHover()
    {
        AkSoundEngine.PostEvent("UI_Select_Option", gameObject);
    }

}
