using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level2";
    public int levelUnlock = 2;

    public void Awake()
    {
        PlayerPrefs.SetInt("levelReached", levelUnlock);
        PlayerPrefs.Save();
    }

    public void Continue()
    {
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
