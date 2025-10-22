using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransferData : MonoBehaviour
{
    public static SceneTransferData instance;
    public int bestMin;
    public int bestSec;
    public float bestMilli;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

}
