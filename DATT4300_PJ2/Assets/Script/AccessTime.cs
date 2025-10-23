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
            if(SceneTransferData.instance.bestSec <= 9)
            {
                lapTime.text = "Time elapsed:0" + SceneTransferData.instance.bestMin + ":0" + SceneTransferData.instance.bestSec + "." + (int)SceneTransferData.instance.bestMilli;
            }
            else
            {
                lapTime.text = "Time elapsed:0" + SceneTransferData.instance.bestMin + ":" + SceneTransferData.instance.bestSec + "." + (int)SceneTransferData.instance.bestMilli;
            }
            
        }
    }
}
