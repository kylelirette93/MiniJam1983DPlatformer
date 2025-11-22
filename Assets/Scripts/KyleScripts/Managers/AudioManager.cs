using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Audio Manager handles playing sound effects and music in the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
    // List to hold actual audio clips for sfx and music.
    List<AudioClip> SFXClips = new List<AudioClip>();
    List<AudioClip> MusicClips = new List<AudioClip>();

    // Dictionarys for quick lookup.
    Dictionary<string, AudioClip> SFXDictionary = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> MusicDictionary = new Dictionary<string, AudioClip>();

    [Header("Audio Sources")]
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource MusicSource;

    private void Awake()
    {
        PopulateAudioLibrary();
    }

    private void PopulateAudioLibrary()
    {
        #region Load Audio Clips
        // Populate each dictionary based on audio clips in lists.
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

    /// <summary>
    /// Retrieves a SFX from dictionary and play it, if it exists.
    /// </summary>
    /// <param name="name">String name of SFX in dictionary.</param>
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

    /// <summary>
    /// Retrives a music track from dictionary and plays it, if it exists.
    /// </summary>
    /// <param name="name">String name of music in dictionary.</param>
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

    /// <summary>
    /// Pauses a music track that's currently playing.
    /// </summary>
    public void PauseMusic()
    {
        // Will used in pause menu.
        MusicSource.Pause();
    }

    /// <summary>
    /// Resumes a paused music track.
    /// </summary>
    public void ResumeMusic()
    {
        // Will be used when resuming from pause menu.
        MusicSource.UnPause();
    }
}
