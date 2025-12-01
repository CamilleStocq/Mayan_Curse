using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    [SerializeField] private Sound[] musicSound, sfxSound;
    [SerializeField] private AudioSource musicSource, sfxSource;

    public static audioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 10f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 10f);

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

        PlayMusic("Flute Maya");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log($" Music '{name}' not found!");
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log($" SFX '{name}' not found!");
        }

        sfxSource.PlayOneShot(s.clip); 
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
