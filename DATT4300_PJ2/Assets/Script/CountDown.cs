using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public GameObject countDown;
    public GameObject lapTimer;
    public GameObject car;

    //public AudioSource audioname;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countStart());
    }


    IEnumerator countStart()
    {
        yield return new WaitForSeconds(.5f);
        countDown.GetComponent<TextMeshProUGUI>().text = "3";
        //add SFX
        countDown.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        yield return new WaitForSeconds(.5f);
        countDown.GetComponent<TextMeshProUGUI>().text = "2";
        //add SFX
        countDown.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        yield return new WaitForSeconds(.5f);
        countDown.GetComponent<TextMeshProUGUI>().text = "1";
        //add SFX
        countDown.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        lapTimer.SetActive(true);
        car.GetComponent<CarController>().enabled = true;
    }
}
