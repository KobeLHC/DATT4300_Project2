using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void RetryGame()
    {
        
        if (HighScoreTable.instance != null)
        {
            HighScoreTable.instance.ExportBestScore();
        }

        SceneManager.LoadScene("Car Selection");
    }

    public void StartGame()
    {
        if (HighScoreTable.instance != null)
        {
            HighScoreTable.instance.ExportBestScore();
        }

        SceneManager.LoadScene("Start");
    }
}