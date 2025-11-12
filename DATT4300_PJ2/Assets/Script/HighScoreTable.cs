using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    [Header("UI References")]
    public Transform entryContainer;
    public Transform entryTemplate;
    private float templateHeight = 75f;

    //public TextMeshProUGUI pos;
    //public TextMeshProUGUI time;

    [System.Serializable]
    public class ScoreEntry
    {
        public int min;
        public int sec;
        public int milli;
        public float totalTime; 
    }

    private List<ScoreEntry> highScores = new List<ScoreEntry>();
    private const string SaveKey = "HighScoreList";

    private void Awake()
    {
       
        LoadScores();

        if (SceneTransferData.instance != null)
        {
            int newMin = SceneTransferData.instance.bestMin;
            int newSec = SceneTransferData.instance.bestSec;
            int newMilli = (int)SceneTransferData.instance.bestMilli;

            AddNewTime(newMin, newSec, newMilli);
        }

       
        DisplayScores();

    }

    private void Start()
    {
        ExportBestScore();
    }

    private void Update()
    {
        // 🔹 Press 'R' to reset leaderboard
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScores();
            //PlayerPrefs.SetInt("BestMin", 0);
            //PlayerPrefs.SetInt("BestSec", 0);
            //PlayerPrefs.SetFloat("BestMilli", 0);
            DisplayScores();
            Debug.Log("Leaderboard has been reset to default!");
        }
    }

    private void ExportBestScore()
    {
        if (highScores.Count == 0 || SceneTransferData.instance == null) return;

        var best = GetBestScore();

        SceneTransferData.instance.SetBestRecord(best.min, best.sec, best.milli);

        PlayerPrefs.SetInt("BestMin", best.min);
        PlayerPrefs.SetInt("BestSec", best.sec);
        PlayerPrefs.SetFloat("BestMilli", best.milli);
        PlayerPrefs.Save();
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(new ScoreListWrapper(highScores));
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(json);
            highScores = wrapper.scores;
        }
        else
        {
            //First time load page
            ResetScores();
        }
    }

    [System.Serializable]
    private class ScoreListWrapper
    {
        public List<ScoreEntry> scores;
        public ScoreListWrapper(List<ScoreEntry> list) { scores = list; }
    }

    private void AddNewTime(int min, int sec, int milli)
    {
        float totalSeconds = min * 60f + sec + milli / 100f;

        ScoreEntry newEntry = new ScoreEntry
        {
            min = min,
            sec = sec,
            milli = milli,
            totalTime = totalSeconds
        };

        // Insert the new time in correct position
        for (int i = 0; i < highScores.Count; i++)
        {
            if (totalSeconds < highScores[i].totalTime || highScores[i].totalTime == 0f)
            {
                highScores.Insert(i, newEntry);
                if (highScores.Count > 5)
                    highScores.RemoveAt(highScores.Count - 1);
                SaveScores();
                return;
            }
        }

        // If slower than all, append to end
        if (highScores.Count < 5)
        {
            highScores.Add(newEntry);
            SaveScores();
        }
    }

    private void ResetScores()
    {
        highScores.Clear();

        for (int i = 0; i < 5; i++)
        {
            highScores.Add(new ScoreEntry { min = 0, sec = 0, milli = 0, totalTime = 0f });
        }

        SaveScores();

        if (SceneTransferData.instance != null)
        {
            SceneTransferData.instance.scoreBestMin = 0;
            SceneTransferData.instance.scoreBestSec = 0;
            SceneTransferData.instance.scoreBestMilli = 0f;
        }

        // 🔹 Reset saved PlayerPrefs
        PlayerPrefs.SetInt("BestMin", 0);
        PlayerPrefs.SetInt("BestSec", 0);
        PlayerPrefs.SetFloat("BestMilli", 0f);
        PlayerPrefs.Save();
    }

    private void DisplayScores()
    {
        
        foreach (Transform child in entryContainer)
        {
            if (child != entryTemplate)
                Destroy(child.gameObject);
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            string rankString = GetRankString(i + 1);
            string timeString = FormatTime(highScores[i].min, highScores[i].sec, highScores[i].milli);

            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;
            entryTransform.Find("lapTimeText").GetComponent<TextMeshProUGUI>().text = timeString;
        }
    }

    private string GetRankString(int rank)
    {
        switch (rank)
        {
            case 1: return "1ST";
            case 2: return "2ND";
            case 3: return "3RD";
            default: return rank + "TH";
        }
    }

    private string FormatTime(int min, int sec, int milli)
    {
        string minStr = (min < 10) ? "0" + min : min.ToString();
        string secStr = (sec < 10) ? "0" + sec : sec.ToString();
        string milliStr = milli.ToString("0");

        return $"{minStr}:{secStr}.{milliStr}";
    }

    public ScoreEntry GetBestScore()
    {
        if (highScores == null || highScores.Count == 0)
            return null;

        return highScores[0];
    }

}
