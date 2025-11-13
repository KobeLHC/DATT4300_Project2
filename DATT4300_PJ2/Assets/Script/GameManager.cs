using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject creditPanel;

    private void Start()
    {
        controlPanel.SetActive(false);
        creditPanel.SetActive(false);
    }


    public void startGame()
    {
        SceneManager.LoadScene("Car Selection");
    }

    public void scoreScene()
    {
        SceneManager.LoadScene("Score");
    }

    public void retry()
    {
        SceneManager.LoadScene("Start");
    }

    //need code for on/off credit and controls panel
    public void controls()
    {
        controlPanel.SetActive(true);
    }

    public void credits()
    {
        creditPanel.SetActive(true);
    }

    public void quitPanels()
    {
        controlPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
