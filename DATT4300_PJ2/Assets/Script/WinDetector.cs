using UnityEngine;

public class WinDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinObject"))
        {
            Debug.Log("Win!");
        }
    }
}