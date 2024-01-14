using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Starting : MonoBehaviour
{
    public Image imageToFade;
    public AudioClip soundToPlay;
    public float fadeDuration = 2.0f; // Dur�e du fondu en secondes
    public float delayBeforeSceneChange = 2.0f; // D�lai avant le changement de sc�ne en secondes

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // D�marrer le fondu automatiquement au lancement du script
        StartFadeOut();
    }

    private void StartFadeOut()
    {
        // Si l'image � fondu est fournie
        if (imageToFade != null)
        {
            // D�marrer la coroutine pour effectuer le fondu
            StartCoroutine(FadeOutCoroutine());
        }
        else
        {
            Debug.LogError("Image to fade not assigned!");
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Initialiser l'alpha de l'image � 1 (compl�tement visible)
        float alpha = 1.0f;
        // Le fondu commence, jouer le son
        PlaySound();
        // Tant que l'alpha n'est pas 0
        while (alpha > 0.0f)
        {
            // Diminuer l'alpha en fonction du temps
            alpha -= Time.deltaTime / fadeDuration;

            // Appliquer l'alpha � l'image
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, alpha);

            yield return null; // Attendre une frame avant la prochaine it�ration
        }

        // Attendre le d�lai avant de changer de sc�ne
        yield return new WaitForSeconds(delayBeforeSceneChange);

        // Changer de sc�ne
        ChangeScene();
    }

    private void PlaySound()
    {
        // Si le son � jouer est fourni
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
        // Charger la sc�ne suivante (� adapter selon votre configuration)
        SceneManager.LoadScene("Intro");
    }
}
