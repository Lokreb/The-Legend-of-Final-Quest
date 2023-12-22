using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> activeAudioSources = new List<AudioSource>();
    private AudioSource backgroundMusic;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    // Ajout de l'événement pour signaler l'arrêt de la musique
    public delegate void MusicStoppedEventHandler(string clipName);
    public static event MusicStoppedEventHandler MusicStoppedEvent;

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

    void Start()
    {
        LoadAudioClips();
    }

    public void PlayMusic(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            AudioClip audioClip = audioClips[clipName];
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSources.Add(audioSource);

            if (backgroundMusic == null)
            {
                backgroundMusic = audioSource;
            }

            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Clip audio not found: " + clipName);
        }
    }

    public void AddAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Add(audioSource);
    }

    void LoadAudioClips()
    {
        // Charger tous les clips audio dans le dossier "Assets/Audio" (ajuster le chemin selon votre structure de dossiers).
        AudioClip[] allAudioClips = Resources.LoadAll<AudioClip>("Audio/Music_Stone_Sample");

        // Ajouter ces clips audio à votre dictionnaire.
        foreach (AudioClip audioClip in allAudioClips)
        {
            audioClips[audioClip.name] = audioClip;
        }
    }

    public void StopMusicForAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
        TryStopBackgroundMusic();
    }

    public void RemoveAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
        TryStopBackgroundMusic();
    }

    public void TryStopBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying && activeAudioSources.Count == 0)
        {
            backgroundMusic.Stop();
            // Ajout de la notification d'arrêt de la musique
            if (MusicStoppedEvent != null)
            {
                MusicStoppedEvent.Invoke(backgroundMusic.clip.name);
            }
        }
    }

    public void StopAllMusic()
    {
        foreach (var audioSource in activeAudioSources)
        {
            Destroy(audioSource);
        }
        activeAudioSources.Clear();
    }
}
