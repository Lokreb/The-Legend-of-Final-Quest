using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> activeAudioSources = new List<AudioSource>();
    private AudioSource backgroundMusic;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>(); // Ajout de ce champ

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

    public void PlayMusic(string clipName) // Modification du paramètre
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
        // Chargez tous les clips audio dans le dossier "Assets/Audio" (ajustez le chemin selon votre structure de dossiers).
        AudioClip[] allAudioClips = Resources.LoadAll<AudioClip>("Audio/Music_Stone_Sample");

        // Ajoutez ces clips audio à votre dictionnaire.
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

    public void StopMusicForAudioClip(AudioClip audioClip)
    {
        // Arrêtez le clip audio spécifié
        foreach (var audioSource in audioSources)
        {
            if (audioSource.clip == audioClip)
            {
                audioSource.Stop();
                activeAudioSources.Remove(audioSource);
                TryStopBackgroundMusic();
                return;
            }
        }
    }

    public void RemoveAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
        TryStopBackgroundMusic();
    }

    public void TryStopBackgroundMusic()
    {
        // Utilise backgroundMusic pour vérifier et arrêter la musique de fond
        if (backgroundMusic != null && backgroundMusic.isPlaying && activeAudioSources.Count == 0)
        {
            backgroundMusic.Stop();
        }
    }

    public void StopAllMusic()
    {
        foreach (var audioSource in audioSources)
        {
            Destroy(audioSource);
        }
        audioSources.Clear();
        activeAudioSources.Clear();

        // Arrêtez la musique de fond si elle est en cours de lecture
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }
}
