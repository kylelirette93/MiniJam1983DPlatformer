using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // List to hold actual audio clips for sfx and music.
    List<AudioClip> SFXClips = new List<AudioClip>();
    List<AudioClip> MusicClips = new List<AudioClip>();

    // Dictionarys for quick lookup.
    Dictionary<string, AudioClip> SFXDictionary = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> MusicDictionary = new Dictionary<string, AudioClip>();

    // Audio source references.
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource MusicSource;

    private void Awake()
    {
        PopulateAudioLibrary();
    }

    private void PopulateAudioLibrary()
    {
        #region Load Audio Clips
        foreach (AudioClip clip in SFXClips)
        {
            SFXDictionary[clip.name] = clip;
        }

        foreach (AudioClip clip in MusicClips)
        {
            MusicDictionary[clip.name] = clip;
        }
        #endregion
    }

    public void PlaySFX(string name)
    {
        if (SFXDictionary.TryGetValue(name, out AudioClip clip))
        {
            SFXSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{name}' not found!");
        }
    }

    public void PlayMusic(string name)
    {
        if (MusicDictionary.TryGetValue(name, out AudioClip clip))
        {
            MusicSource.clip = clip;
            MusicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music '{name}' not found!");
        }
    }

    public void PauseMusic()
    {
        // Will used in pause menu.
        MusicSource.Pause();
    }

    public void ResumeMusic()
    {
        // Will be used when resuming from pause menu.
        MusicSource.UnPause();
    }
}
