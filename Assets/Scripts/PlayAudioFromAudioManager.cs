using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioFromAudioManager : MonoBehaviour
{
    public Sound.AudioObject target;

    public void Play()
    {
        AudioManager.instance.Play(target);
    }

    public void Play(Sound.AudioObject audioName)
    {
        AudioManager.instance.Play(audioName);
    }
}
