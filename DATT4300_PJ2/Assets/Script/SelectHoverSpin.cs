using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SelectHoverSpin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CarSpinner carSpinner;
    public int carID = 1; // 1–4 depending on the button

    [TextArea]
    public string description; // text for THIS car
    public TextMeshProUGUI descriptionText; // UI text object

    public void OnPointerEnter(PointerEventData eventData)
    {
        carSpinner.StartSpin();

        if (descriptionText != null)
        {
            descriptionText.text = description;
            descriptionText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        carSpinner.StopSpin();

        if (descriptionText != null)
        {
            descriptionText.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerPrefs.SetInt("CarChoice", carID);
        Debug.Log("Selected Car: " + carID);
    }
}