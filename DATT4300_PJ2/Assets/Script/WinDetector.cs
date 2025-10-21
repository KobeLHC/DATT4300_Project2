using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinDetector : MonoBehaviour
{
    public GameObject winLine;
    public GameObject midLine;

    public GameObject bestMinDisplay;
    public GameObject bestSecDisplay;
    public GameObject bestMilliDisplay;

    public int lapCount = 0;
    public GameObject lapCountDisplay;

    private void Start()
    {
        winLine.SetActive(false);
        midLine.SetActive(true);
    }

    private void Update()
    {
        lapCountDisplay.GetComponent<TextMeshProUGUI>().text = lapCount + "/2";
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MidObject"))
        {
            winLine.SetActive(true);
            midLine.SetActive(false);
        }

        if (other.CompareTag("WinObject"))
        {
            midLine.SetActive(true);
            winLine.SetActive(false);

            lapCount += 1;

            //best time
            if(LapTimeManager.secCount <= 9)
            {
                bestSecDisplay.GetComponent<TextMeshProUGUI>().text = "0" + LapTimeManager.secCount + ".";
            }
            else
            {
                bestSecDisplay.GetComponent<TextMeshProUGUI>().text = "" + LapTimeManager.secCount + ".";
            }

            if (LapTimeManager.minCount <= 9)
            {
                bestMinDisplay.GetComponent<TextMeshProUGUI>().text = "0" + LapTimeManager.minCount + ":";
            }
            else
            {
                bestMinDisplay.GetComponent<TextMeshProUGUI>().text = "" + LapTimeManager.minCount + ":";
            }
                bestMilliDisplay.GetComponent<TextMeshProUGUI>().text = "" + ((int)LapTimeManager.milliCount);
            
            LapTimeManager.minCount = 0;
            LapTimeManager.secCount = 0;
            LapTimeManager.milliCount = 0;

            if(lapCount > 2)
            {
                SceneManager.LoadScene("Score");
            }
        }
    }
}