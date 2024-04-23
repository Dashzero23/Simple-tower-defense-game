using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level2";
    public int levelUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelUnlock);
        sceneFader.FadeTo(nextLevel);
        PlayerPrefs.Save();
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
