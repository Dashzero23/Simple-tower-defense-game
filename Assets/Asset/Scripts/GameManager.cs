using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
        { 
            return; 
        }
        
        /*if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (Input.GetKeyDown("r"))
        {
            WinLevel();
        }*/

        if (PlayerStat.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;

        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;

        completeLevelUI.SetActive(true);
    }
}
