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

    public static int compareMin = 0;
    public static int compareSec = 0;
    public static float compareMilli = 0f;

    public int lapCount = 0;
    public GameObject lapCountDisplay;

    private void Start()
    {
        winLine.SetActive(false);
        midLine.SetActive(true);
    }

    private void Update()
    {
        lapCountDisplay.GetComponent<TextMeshProUGUI>().text = lapCount + "/1";
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

            // 🔹 TRACK IF THIS IS A NEW BEST LAP
            bool isNewBest = false;

            // ---------------- MINUTES ----------------
            if (LapTimeManager.minCount < compareMin || compareMin == 0)
            {
                compareMin = LapTimeManager.minCount;
                isNewBest = true;

                if (LapTimeManager.minCount <= 9)
                    bestMinDisplay.GetComponent<TextMeshProUGUI>().text = "0" + LapTimeManager.minCount + ":";
                else
                    bestMinDisplay.GetComponent<TextMeshProUGUI>().text = LapTimeManager.minCount + ":";
            }

            // ---------------- SECONDS ----------------
            if ((LapTimeManager.secCount < compareSec || compareSec == 0)
                && LapTimeManager.minCount == compareMin)
            {
                compareSec = LapTimeManager.secCount;
                isNewBest = true;

                if (LapTimeManager.secCount <= 9)
                    bestSecDisplay.GetComponent<TextMeshProUGUI>().text = "0" + LapTimeManager.secCount + ".";
                else
                    bestSecDisplay.GetComponent<TextMeshProUGUI>().text = LapTimeManager.secCount + ".";
            }

            // ---------------- MILLISECONDS ----------------
            if (LapTimeManager.milliCount < compareMilli || compareMilli == 0f)
            {
                compareMilli = LapTimeManager.milliCount;
                isNewBest = true;

                bestMilliDisplay.GetComponent<TextMeshProUGUI>().text =
                    ((int)LapTimeManager.milliCount).ToString();
            }

            // ⭐ SAVE GHOST IF NEW BEST TIME
            if (isNewBest)
            {
                GhostRecorder rec = FindObjectOfType<GhostRecorder>();
                if (rec != null)
                {
                    rec.StopRecording();
                    rec.SaveAsBest();
                }
            }

            // Reset timer for next lap
            LapTimeManager.minCount = 0;
            LapTimeManager.secCount = 0;
            LapTimeManager.milliCount = 0;

            // If finished → go to score screen
            if (lapCount > 1)
            {
                if (SceneTransferData.instance != null)
                {
                    SceneTransferData.instance.SetBestTime(compareMin, compareSec, compareMilli);
                }

                SceneManager.LoadScene("Score");
            }
        }
    }
}
