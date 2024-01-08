using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> activeAudioSources = new List<AudioSource>();
    private AudioSource backgroundMusic;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>(); // Ajout de ce champ
    public Dictionary<string, AudioClip> bossMusicClips = new Dictionary<string, AudioClip>();
    private bool introPlayed = false;
    public bool bossMusicPlaying;
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
        LoadBossMusicClips(); // Chargez les clips audio des boss
        SceneManager.sceneLoaded += OnSceneLoaded; // Abonnez-vous à l'événement sceneLoaded
    }
    void Update()
    {

        string sceneName = SceneManager.GetActiveScene().name;
        /*
        if (sceneName == "Battle" && GetComponent<GameManager>() != null)
        {
            int currentBoss = GetComponent<GameManager>().boss;

            if (currentBoss == 0)
            {
                PlayMusic("1stBoss");
            }
            else if (currentBoss == 1)
            {   
                PlayMusic("2ndBoss");
              
            }
            else if (currentBoss == 2)
            {
                if (!introPlayed)
                {
                    PlayMusic("3rdBossIntro", false);
                    introPlayed = true;
                }
                else if (!bossMusicPlaying)
                {
                    PlayMusic("3rdBossLoop");
                    bossMusicPlaying = true;
                }
            }
        }
        else
        {
            introPlayed = false;
            bossMusicPlaying = false;
        }*/
    }

    public void PlayMusic(string clipName, bool loop = true)
    {
        if (audioClips.ContainsKey(clipName))
        {
            AudioClip audioClip = audioClips[clipName];
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            audioSources.Add(audioSource);

            if (backgroundMusic == null)
            {
                backgroundMusic = audioSource;
            }

            audioSource.Play();

            // Si l'intro est jouée et la musique doit boucler, détruire l'intro
            if (introPlayed && loop)
            {
                Destroy(audioSources[0]);
                audioSources.RemoveAt(0);
            }

            // Ajoutez l'AudioSource à la liste des AudioSources actifs
            AddAudioSource(audioSource);
        }
        else
        {
            Debug.LogWarning("Clip audio not found: " + clipName);

        }
    }
    public void PlayBossMusic(string bossName)
    {
        if (bossMusicClips.ContainsKey(bossName))
        {
            Debug.LogWarning("play music value" + bossMusicClips[bossName].name);
            PlayMusic(bossMusicClips[bossName].name);
        }
        else
        {
            Debug.LogWarning("Boss music clip not found for boss: " + bossName);
            
        }
    }
    public void AddAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Add(audioSource);
    }

    void LoadAudioClips()
    {
        // Chargez tous les clips audio dans le dossier "Assets/Audio" (ajustez le chemin selon votre structure de dossiers).
        AudioClip[] allAudioClips = Resources.LoadAll<AudioClip>("Audio");
        
        // Ajoutez ces clips audio à votre dictionnaire.
        foreach (AudioClip audioClip in allAudioClips)
        {
            audioClips[audioClip.name] = audioClip;
        }
    }

    void LoadBossMusicClips()
    {
        // Ajoutez les clips audio des boss à votre dictionnaire.
        bossMusicClips["1stBoss"] = Resources.Load<AudioClip>("Audio/BossMusic/1stBoss");
        bossMusicClips["2ndBoss"] = Resources.Load<AudioClip>("Audio/BossMusic/2ndBoss");
        bossMusicClips["3rdBossIntro"] = Resources.Load<AudioClip>("Audio/BossMusic/3rdBossIntro");
        bossMusicClips["3rdBossLoop"] = Resources.Load<AudioClip>("Audio/BossMusic/3rdBossLoop");
        // Ajoutez d'autres boss au besoin
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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cherchez le GameManager dans la scène actuelle
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (scene.name == "Battle" && gameManager != null)
        {
            StopAllMusic();
            int numBoss = gameManager.boss;

            if (numBoss == 0)
            {
                PlayBossMusic("1stBoss"); // Jouez la musique du premier boss
                
            }
            else if (numBoss == 1)
            {
                PlayBossMusic("2ndBoss"); // Jouez la musique du deuxième boss
            }
            else if (numBoss == 2)
            {
                PlayMusic("3rdBossintro", false);
                introPlayed = true;
            }
            else if (scene.name == "map" && gameManager != null)
            {
                StopAllMusic();
            }
            else
            {
                Debug.LogWarning("Clip audio not found: limite broken");
            }
        }
    }
}

