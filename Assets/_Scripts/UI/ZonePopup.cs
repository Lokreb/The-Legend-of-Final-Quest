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
        // D�sactive le popup au d�but
        gameObject.SetActive(false);

        // R�cup�re ou ajoute le CanvasGroup pour g�rer le fondu
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Initialise l'alpha � z�ro
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
        // Fondu d'entr�e
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Attends pendant la dur�e d'affichage
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

        // D�sactive le popup
        gameObject.SetActive(false);
    }
}


