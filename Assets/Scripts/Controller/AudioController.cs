using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider musicSlider, effectSlider;
    public AudioSource backgroundMusic, buttonClickSound;
    public static float musicLevel = 0, effectLevel = 0;

    public static AudioSource playingMusic;

    void Update()
    {
        musicLevel = musicSlider.value;
        effectLevel = effectSlider.value;
        
        if(playingMusic != null)
        {
            playingMusic.volume = musicLevel;
        }
        if(buttonClickSound != null)
        {
            buttonClickSound.volume = effectLevel;
        }
    }

    public void Clicked()
    {
        if(buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
    }
}
