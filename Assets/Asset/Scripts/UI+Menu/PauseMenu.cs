using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject tutorialUI;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    void Start()
    {
        if (tutorialUI && PlayerPrefs.GetString("first") == "true")
        {
            tutorialUI.SetActive(true);
            Debug.Log(PlayerPrefs.GetString("first"));
            Time.timeScale = 0f;
        }
    }
    void Update()
    {
        if (PlayerPrefs.GetString("first") == "true")
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                exitTutorial();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

    public void exitTutorial()
    {
        PlayerPrefs.SetString("first", "false");
        Debug.Log(PlayerPrefs.GetString("first"));
        tutorialUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
