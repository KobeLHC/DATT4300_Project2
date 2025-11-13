using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMeshSwap : MonoBehaviour
{
    [Header("Car Meshes")]
    public Mesh Car1;
    public Mesh Car2;
    public Mesh Car3;
    public Mesh Car4;

    private MeshFilter meshFilter;


    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (PlayerPrefs.GetInt("CarChoice") == 1)
        {
            meshFilter.mesh = Car1;
        }

        else if(PlayerPrefs.GetInt("CarChoice") == 2)
        {
            meshFilter.mesh = Car2;
        }

        else if (PlayerPrefs.GetInt("CarChoice") == 3)
        {
            meshFilter.mesh = Car3;
        }

        else
        {
            meshFilter.mesh = Car4;
        }
    }

    
}
