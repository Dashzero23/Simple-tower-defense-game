using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader Fader;

    public GameObject normalMode;
    public GameObject hardMode;
    public Button normalButton;
    public Button hardButton;

    public Button[] levelButtons;
    public Button[] levelHardButtons;
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        normalButton.interactable = false;
        hardMode.SetActive(false);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }

        for (int i = 0; i < levelHardButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelHardButtons[i].interactable = false;
            }
        }
    }
    public void Select(string levelName)
    {
        Fader.FadeTo(levelName);
    }

    public void LevelToggle()
    {
        if (normalMode.activeSelf)
        {
            normalMode.SetActive(false);
            hardMode.SetActive(true);
            normalButton.interactable = true;
            hardButton.interactable = false;
        }
        
        else
        {
            normalMode.SetActive(true);
            hardMode.SetActive(false);
            normalButton.interactable = false;
            hardButton.interactable = true;
        }
    }
}
