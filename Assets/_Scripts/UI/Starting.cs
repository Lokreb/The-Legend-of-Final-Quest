using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Starting : MonoBehaviour
{
    public Image imageToFade;
    public AudioClip soundToPlay;
    public float fadeDuration = 2.0f; // Durée du fondu en secondes
    public float delayBeforeSceneChange = 2.0f; // Délai avant le changement de scène en secondes

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Démarrer le fondu automatiquement au lancement du script
        StartFadeOut();
    }

    private void StartFadeOut()
    {
        // Si l'image à fondu est fournie
        if (imageToFade != null)
        {
            // Démarrer la coroutine pour effectuer le fondu
            StartCoroutine(FadeOutCoroutine());
        }
        else
        {
            Debug.LogError("Image to fade not assigned!");
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Initialiser l'alpha de l'image à 1 (complètement visible)
        float alpha = 1.0f;
        // Le fondu commence, jouer le son
        PlaySound();
        // Tant que l'alpha n'est pas 0
        while (alpha > 0.0f)
        {
            // Diminuer l'alpha en fonction du temps
            alpha -= Time.deltaTime / fadeDuration;

            // Appliquer l'alpha à l'image
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, alpha);

            yield return null; // Attendre une frame avant la prochaine itération
        }

        // Attendre le délai avant de changer de scène
        yield return new WaitForSeconds(delayBeforeSceneChange);

        // Changer de scène
        ChangeScene();
    }

    private void PlaySound()
    {
        // Si le son à jouer est fourni
        if (soundToPlay != null && audioSource != null)
        {
            // Jouer le son
            audioSource.PlayOneShot(soundToPlay);
        }
        else
        {
            Debug.LogError("Sound to play or AudioSource not assigned!");
        }
    }

    private void ChangeScene()
    {
        // Charger la scène suivante (à adapter selon votre configuration)
        SceneManager.LoadScene("Intro");
    }
}
