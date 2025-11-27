using UnityEngine;

public class GhostCar : MonoBehaviour
{
    private float playbackTimer = 0f;
    private bool playing = false;

    void Start()
    {
        // No ghost yet? Hide.
        if (GhostData.bestPositions == null)
        {
            gameObject.SetActive(false);
            return;
        }

        StartPlayback();
    }

    public void StartPlayback()
    {
        if (GhostData.bestPositions == null || GhostData.bestPositions.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        Debug.Log("👻 Ghost playback started");
        playbackTimer = 0f;
        playing = true;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!playing) return;

        int index = Mathf.FloorToInt(playbackTimer / GhostData.interval);

        if (index >= GhostData.bestPositions.Count)
        {
            playing = false;
            gameObject.SetActive(false);
            return;
        }

        transform.position = GhostData.bestPositions[index];
        transform.rotation = GhostData.bestRotations[index];

        playbackTimer += Time.deltaTime;
    }
}
