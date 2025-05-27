using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SonidoAjustes : MonoBehaviour
{
    public Slider musicSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        musicSlider.value = 0.5f;
        SetMusicVolume(musicSlider.value);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }


    public void SetMusicVolume(float volume)
    {
        float minVolume = 0.0001f;
      audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(volume, minVolume)) * 20);
    }
}

