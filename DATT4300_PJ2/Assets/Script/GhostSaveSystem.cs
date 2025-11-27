using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public static class GhostSaveSystem
{
    private static string path = Application.persistentDataPath + "/ghost.dat";

    [System.Serializable]
    class GhostFileData
    {
        public List<Vector3> positions;
        public List<Quaternion> rotations;
        public float interval;
    }

    public static void SaveGhost()
    {
        if (GhostData.bestPositions == null)
        {
            Debug.LogWarning("⚠ Tried to save ghost but no data exists.");
            return;
        }

        GhostFileData data = new GhostFileData();
        data.positions = GhostData.bestPositions;
        data.rotations = GhostData.bestRotations;
        data.interval = GhostData.interval;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("💾 Ghost saved to: " + path);
    }

    public static bool LoadGhost()
    {
        if (!File.Exists(path))
        {
            Debug.Log("❌ No saved ghost file found.");
            return false;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        GhostFileData data = formatter.Deserialize(stream) as GhostFileData;
        stream.Close();

        GhostData.bestPositions = data.positions;
        GhostData.bestRotations = data.rotations;
        GhostData.interval = data.interval;

        Debug.Log("📂 Ghost loaded (" + data.positions.Count + " frames)");
        return true;
    }
}
