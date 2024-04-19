using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Level1";
    public SceneFader sceneFader;
    public void Start()
    {
        if (PlayerPrefs.GetString("first") != "false")
        {
            PlayerPrefs.SetString("first", "true");
        }
    }

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
