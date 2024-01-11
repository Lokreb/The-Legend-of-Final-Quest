using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZonePopup : MonoBehaviour
{
    public TextMeshProUGUI zoneText;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        // Désactive le popup au début
        gameObject.SetActive(false);

        // Récupère ou ajoute le CanvasGroup pour gérer le fondu
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Initialise l'alpha à zéro
        canvasGroup.alpha = 0f;
    }

    public void ShowPopup(string zoneName)
    {
        // Affiche le popup avec fondu
        zoneText.text = zoneName;
        gameObject.SetActive(true);
        StartCoroutine(FadeInAndOut());
    }

    System.Collections.IEnumerator FadeInAndOut()
    {
        // Fondu d'entrée
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Attends pendant la durée d'affichage
        yield return new WaitForSeconds(3f);

        // Fondu de sortie
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // Désactive le popup
        gameObject.SetActive(false);
    }
}


