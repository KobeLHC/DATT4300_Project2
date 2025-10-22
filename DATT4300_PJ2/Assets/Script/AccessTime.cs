using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccessTime : MonoBehaviour
{
    public TextMeshProUGUI lapTime;

    void Start()
    {
        if (SceneTransferData.instance != null)
        {
            lapTime.text = "Time elapsed:" + SceneTransferData.instance.bestMin + ":" + SceneTransferData.instance.bestSec + "." + (int)SceneTransferData.instance.bestMilli;
        }
    }
}
