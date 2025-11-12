using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransferData : MonoBehaviour
{
    public static SceneTransferData instance;
    public int bestMin;
    public int bestSec;
    public float bestMilli;

    public int scoreBestMin;
    public int scoreBestSec;
    public float scoreBestMilli;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            scoreBestMin = PlayerPrefs.GetInt("BestMin", 0);
            scoreBestSec = PlayerPrefs.GetInt("BestSec", 0);
            scoreBestMilli = PlayerPrefs.GetFloat("BestMilli", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBestTime(int min, int sec, float milli)
    {
        bestMin = min;
        bestSec = sec;
        bestMilli = milli;
    }

    public void SetBestRecord(int min, int sec, float milli)
    {
        scoreBestMin = min;
        scoreBestSec = sec;
        scoreBestMilli = milli;

        PlayerPrefs.SetInt("BestMin", min);
        PlayerPrefs.SetInt("BestSec", sec);
        PlayerPrefs.SetFloat("BestMilli", milli);
        PlayerPrefs.Save();
    }

}
