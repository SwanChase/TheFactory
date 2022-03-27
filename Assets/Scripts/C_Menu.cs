using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_Menu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quiting()
    {
        Application.Quit();
    }

    public void Sound()
    {

    }

    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }





}
