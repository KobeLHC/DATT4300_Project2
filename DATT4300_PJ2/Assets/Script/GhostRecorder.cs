using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [Header("Ghost Recording")]
    public float recordInterval = 0.05f;

    private float timer = 0f;
    private bool isRecording = true;

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    void Update()
    {
        if (!isRecording) return;

        timer += Time.deltaTime;

        if (timer >= recordInterval)
        {
            positions.Add(transform.position);
            rotations.Add(transform.rotation);
            timer = 0f;
        }
    }

    public void ResetRecording()
    {
        positions.Clear();
        rotations.Clear();
        timer = 0f;
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void SaveAsBest()
    {
        if (positions.Count == 0)
        {
            Debug.LogWarning("⚠ No ghost data recorded this run.");
            return;
        }

        GhostData.bestPositions = new List<Vector3>(positions);
        GhostData.bestRotations = new List<Quaternion>(rotations);
        GhostData.interval = recordInterval;

        Debug.Log("⭐ NEW BEST GHOST SAVED (" + positions.Count + " frames)");
    }
}
