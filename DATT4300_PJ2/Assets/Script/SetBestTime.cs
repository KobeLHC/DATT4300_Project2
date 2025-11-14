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
        int min = PlayerPrefs.GetInt("BestMin", 0);
        int sec = PlayerPrefs.GetInt("BestSec", 0);
        float milli = PlayerPrefs.GetFloat("BestMilli", 0f);

        string minStr = (min < 10) ? "0" + min : min.ToString();
        string secStr = (sec < 10) ? "0" + sec : sec.ToString();
        string milliStr = milli.ToString("0");

        displayMin.text = minStr + ":";
        displaySec.text = secStr + ".";
        displayMilli.text = milliStr;
    }
}
