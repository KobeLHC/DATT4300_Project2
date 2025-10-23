using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingMenu;
    public bool isPaused;
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true); 
        isPaused = true;
    }

    public void setting()
    {
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void resume()
    {
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
    }
}
