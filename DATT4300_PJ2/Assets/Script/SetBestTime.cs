using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetBestTime : MonoBehaviour
{
    public TextMeshProUGUI displayMin;
    public TextMeshProUGUI displaySec;
    public TextMeshProUGUI displayMilli;

    void Awake()
    {
        int min = 0;
        int sec = 0;
        float milli = 0;

        if (SceneTransferData.instance != null)
        {
            min = SceneTransferData.instance.scoreBestMin;
            sec = SceneTransferData.instance.scoreBestSec;
            milli = SceneTransferData.instance.scoreBestMilli;
        }
        else if (PlayerPrefs.HasKey("BestMin"))
        {
            min = PlayerPrefs.GetInt("BestMin");
            sec = PlayerPrefs.GetInt("BestSec");
            milli = PlayerPrefs.GetFloat("BestMilli");
        }
       
        string minStr = (min < 10) ? "0" + min : min.ToString();
        string secStr = (sec < 10) ? "0" + sec : sec.ToString();
        string milliStr = milli.ToString("0");

        displayMin.text = minStr + ":";
        displaySec.text = secStr + ".";
        displayMilli.text = milliStr;
    }
}
