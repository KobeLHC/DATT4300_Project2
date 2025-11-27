using UnityEngine;

public class TurnArrowUI : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    void Start()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);
    }

    void Update()
    {
        bool turningLeft = Input.GetKey(KeyCode.A);
        bool turningRight = Input.GetKey(KeyCode.L);
        bool movingForward = Input.GetKey(KeyCode.W);
        bool movingBackward = Input.GetKey(KeyCode.K);

        leftArrow.SetActive(turningLeft);
        rightArrow.SetActive(turningRight);
        upArrow.SetActive(movingForward);
        downArrow.SetActive(movingBackward);
    }
}
