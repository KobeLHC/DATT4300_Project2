using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCar : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("CarChoice", 0);
        Debug.Log("Reset Selection");
    }

    public void selectCar1()
    {
        PlayerPrefs.SetInt("CarChoice", 1);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }

    public void selectCar2()
    {
        PlayerPrefs.SetInt("CarChoice", 2);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }

    public void selectCar3()
    {
        PlayerPrefs.SetInt("CarChoice", 3);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }

    public void selectCar4()
    {
        PlayerPrefs.SetInt("CarChoice", 4);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }
}
