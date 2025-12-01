using UnityEngine;
using UnityEngine.UI;

public class UISliderSound : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 10f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 10f);

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    public void UpdateMusicVolume()
    {
        if (audioManager.Instance != null)
        {
            audioManager.Instance.MusicVolume(musicSlider.value);
        }
    }

    public void UpdateSFXVolume()
    {
        if (audioManager.Instance != null)
        {
            audioManager.Instance.SFXVolume(sfxSlider.value);
        }
    }

    public void ToggleMusic()
    {
        audioManager.Instance?.ToggleMusic();
    }

    public void ToggleSFX()
    {
        audioManager.Instance?.ToggleSFX();
    }
}
