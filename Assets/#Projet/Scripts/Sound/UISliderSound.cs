using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UISliderSound : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;

    public void ToggleMusic()
    {
        audioManager.Instance.ToggleMusic();
    }

    public void MusicVolume()
    {
        audioManager.Instance.MusicVolume(_musicSlider.value);
    }
}
