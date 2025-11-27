using System.IO;
using UnityEngine;
using System;

public static class GhostSaveSystem
{
    // Use JSON so we don't get weird BinaryFormatter errors
    private static string path = Path.Combine(Application.persistentDataPath, "ghost.json");

    [Serializable]
    private class GhostFileData
    {
        public float interval;
        public Vector3[] positions;
        public Quaternion[] rotations;
    }

    public static void SaveGhost()
    {
        if (GhostData.bestPositions == null || GhostData.bestPositions.Count == 0)
        {
            Debug.LogWarning("⚠ Tried to save ghost but no data exists.");
            return;
        }

        GhostFileData data = new GhostFileData
        {
            interval = GhostData.interval,
            positions = GhostData.bestPositions.ToArray(),
            rotations = GhostData.bestRotations.ToArray()
        };

        try
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
            Debug.Log("💾 Ghost saved to: " + path);
        }
        catch (Exception e)
        {
            Debug.LogWarning("GhostSaveSystem.SaveGhost error: " + e.Message);
        }
    }

    public static bool LoadGhost()
    {
        if (!File.Exists(path))
        {
            Debug.Log("❌ No saved ghost file found at: " + path);
            return false;
        }

        try
        {
            string json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("⚠ Ghost file empty.");
                return false;
            }

            GhostFileData data = JsonUtility.FromJson<GhostFileData>(json);
            if (data == null || data.positions == null || data.positions.Length == 0)
            {
                Debug.LogWarning("⚠ Ghost file invalid.");
                return false;
            }

            GhostData.interval = data.interval;
            GhostData.bestPositions = new System.Collections.Generic.List<Vector3>(data.positions);
            GhostData.bestRotations = new System.Collections.Generic.List<Quaternion>(data.rotations);

            Debug.Log("📂 Ghost loaded (" + data.positions.Length + " frames)");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("GhostSaveSystem.LoadGhost error: " + e.Message);
            return false;
        }
    }
}
