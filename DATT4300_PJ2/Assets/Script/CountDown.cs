using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public GameObject countDown;
    public GameObject lapTimer;
    public GameObject car;

        private Rigidbody rb;


    public AudioSource countSfx;
    public AudioSource goSfx;


    // Start is called before the first frame update
    void Start()
    {
         rb = car.GetComponent<Rigidbody>();
        rb.isKinematic = true;


        StartCoroutine(countStart());
    }


    IEnumerator countStart()
    {
        
        yield return new WaitForSeconds(.5f);
        countDown.GetComponent<TextMeshProUGUI>().text = "3";
        countSfx.Play();
        countDown.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        yield return new WaitForSeconds(.5f);
        countDown.GetComponent<TextMeshProUGUI>().text = "2";
        countSfx.Play();
        countDown.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        yield return new WaitForSeconds(.5f);
        countDown.GetComponent<TextMeshProUGUI>().text = "1";
        countSfx.Play();
        countDown.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        goSfx.Play();
        lapTimer.SetActive(true);
                rb.isKinematic = false;



    }
}
