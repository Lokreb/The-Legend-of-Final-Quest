using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> activeAudioSources = new List<AudioSource>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip audioClip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSources.Add(audioSource);
        audioSource.Play();
    }

    public void AddAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Add(audioSource);
    }

    public void RemoveAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
        TryStopBackgroundMusic();
    }

    public void TryStopBackgroundMusic()
    {
        if (audioSources.Count > 0 && audioSources[0].isPlaying && activeAudioSources.Count == 0)
        {
            audioSources[0].Stop();
        }
    }
    public void StopMusicForAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
        TryStopBackgroundMusic();
    }
    public void StopAllMusic()
    {
        foreach (var audioSource in activeAudioSources)
        {
            Destroy(audioSource);
        }
        activeAudioSources.Clear();

        // Arrêtez la musique de fond si elle est en cours de lecture
        if (audioSources.Count > 0 && audioSources[0].isPlaying)
        {
            audioSources[0].Stop();
        }
    }
}
