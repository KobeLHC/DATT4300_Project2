using UnityEngine.EventSystems;
using UnityEngine;


public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound(clickSound);
    }
}