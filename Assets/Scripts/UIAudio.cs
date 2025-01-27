using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sound.AudioObject clickAudioName;
    public Sound.AudioObject hoverEnterAudioName;
    public Sound.AudioObject hoverExitAudioName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickAudioName != Sound.AudioObject.None)
        {
            AudioManager.instance.Play(clickAudioName);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEnterAudioName != Sound.AudioObject.None)
        {
            AudioManager.instance.Play(hoverEnterAudioName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverExitAudioName != Sound.AudioObject.None)
        {
            AudioManager.instance.Play(hoverExitAudioName);
        }
    }
}
