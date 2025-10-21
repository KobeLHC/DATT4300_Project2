using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetector : MonoBehaviour
{
    public GameObject winLine;
    private void Start()
    {
        winLine.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MidObject"))
        {
            winLine.SetActive(true);
        }

        if (other.CompareTag("WinObject"))
        {
            SceneManager.LoadScene("Score");
        }
    }
}