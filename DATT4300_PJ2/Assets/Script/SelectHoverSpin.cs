using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHoverSpin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CarSpinner carSpinner;
    public int carID = 1; // 1–4 depending on the button

    public void OnPointerEnter(PointerEventData eventData)
    {
        carSpinner.StartSpin();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        carSpinner.StopSpin();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerPrefs.SetInt("CarChoice", carID);
        Debug.Log("Selected Car: " + carID);
    }
}