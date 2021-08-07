using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    
    private void Start()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1);
        // audioMixer.SetFloat("BgmVolume", 20 * (float) Math.Log10(bgmVolume));
        // audioMixer.SetFloat("SfxVolume", 20 *  (float) Math.Log10(sfxVolume));
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    public void SetBgmVolume(float volume)
    {
        audioMixer.SetFloat("BgmVolume", 20 * (float) Math.Log10(volume));
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }
    
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", 20 *  (float) Math.Log10(volume));
        PlayerPrefs.SetFloat("SfxVolume", volume);

    }
}
