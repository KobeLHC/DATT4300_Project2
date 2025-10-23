using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    public static int minCount;
    public static int secCount;
    public static float milliCount;
    public static string milliDisplay;

    public GameObject minBox;
    public GameObject secBox;
    public GameObject milliBox;

    private void Start()
    {
        minCount = 0;
        secCount = 0;
        milliCount = 0f;
    }

    void Update()
    {
        milliCount += Time.deltaTime * 10;
        milliDisplay = milliCount.ToString("F0");
        milliBox.GetComponent<TextMeshProUGUI>().text = " " + milliDisplay;

        if (milliCount >= 9)
        {
            milliCount = 0;
            secCount += 1;
        }

        if(secCount <= 9)
        {
            secBox.GetComponent<TextMeshProUGUI>().text = "0" + secCount + ".";
        }
        else
        {
            secBox.GetComponent<TextMeshProUGUI>().text = "" + secCount + ".";
        }

        if(secCount > 59)
        {
            secCount = 0;
            minCount += 1;
        }

        if (minCount <= 9)
        {
            minBox.GetComponent<TextMeshProUGUI>().text = "0" + minCount + ":";
        }
        else
        {
            minBox.GetComponent<TextMeshProUGUI>().text = "" + minCount + ":";
        }

    }
}
