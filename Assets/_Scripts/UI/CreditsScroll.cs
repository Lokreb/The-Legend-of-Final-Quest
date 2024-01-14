using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Animations;

public class CreditsScroll : MonoBehaviour
{
    public TMP_Text creditsText;
    public string[] FristPart;
    public string[] SecondPart;
    public string[] ThrirdPart;
    public string[] ForthPart;
    public float scrollSpeed = 2f;
    public float delayBeforeSceneChange = 5f;
    public AnimatorController[] Token0;
    public AnimatorController[] Token1;
    public AnimatorController[] Token2;
    public GameObject[] TokenList;
    public CanvasGroup canvasGroup;
    public GameObject[] BackGround;
    public float fadeInDuration = 1f;

    private void Start()
    {
    
        canvasGroup.alpha = 0f;
        StartCoroutine(ScrollCredits());
    }

    IEnumerator ScrollCredits()
    {
        creditsText.text = ""; // Vider le texte
        TokenList[0].GetComponent<Animator>().runtimeAnimatorController = Token0[0];
        TokenList[1].GetComponent<Animator>().runtimeAnimatorController = Token1[0];
        TokenList[2].GetComponent<Animator>().runtimeAnimatorController = Token2[0];
        BackGround[0].SetActive(true);
        //
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        //
        foreach (var entry in FristPart)
        {
            creditsText.text += entry + "\n" + "\n"; // Ajouter le nom et le r�le au texte

            yield return new WaitForSeconds(scrollSpeed); // Attendre 1 seconde entre chaque entr�e
        }

        yield return new WaitForSeconds(delayBeforeSceneChange);
        creditsText.text = ""; // Vider le texte
        elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f; 
        BackGround[0].SetActive(false);
        TokenList[0].GetComponent<Animator>().runtimeAnimatorController = Token0[1];
        TokenList[1].GetComponent<Animator>().runtimeAnimatorController = Token1[1];
        TokenList[2].GetComponent<Animator>().runtimeAnimatorController = Token2[1];
        BackGround[1].SetActive(true);
        elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        foreach (var entry in SecondPart)
        {
            creditsText.text += entry + "\n" + "\n"; // Ajouter le nom et le r�le au texte

            yield return new WaitForSeconds(scrollSpeed); // Attendre 1 seconde entre chaque entr�e
        }

        yield return new WaitForSeconds(delayBeforeSceneChange);
        creditsText.text = ""; // Vider le texte
        elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        BackGround[1].SetActive(false);
        TokenList[0].GetComponent<Animator>().runtimeAnimatorController = Token0[2];
        TokenList[1].GetComponent<Animator>().runtimeAnimatorController = Token1[2];
        TokenList[2].GetComponent<Animator>().runtimeAnimatorController = Token2[2];
        BackGround[2].SetActive(true);
        elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        foreach (var entry in ThrirdPart)
        {
            creditsText.text += entry + "\n" + "\n"; // Ajouter le nom et le r�le au texte

            yield return new WaitForSeconds(scrollSpeed); // Attendre 1 seconde entre chaque entr�e
        }

        yield return new WaitForSeconds(delayBeforeSceneChange);
        creditsText.text = ""; // Vider le texte
        elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        BackGround[2].SetActive(false);
        TokenList[0].GetComponent<Animator>().runtimeAnimatorController = Token0[3];
        TokenList[1].GetComponent<Animator>().runtimeAnimatorController = Token1[3];
        TokenList[2].GetComponent<Animator>().runtimeAnimatorController = Token2[3];
        BackGround[0].SetActive(true);
        elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        foreach (var entry in ForthPart)
        {
            creditsText.text += entry + "\n" + "\n"; // Ajouter le nom et le r�le au texte

            yield return new WaitForSeconds(scrollSpeed); // Attendre 1 seconde entre chaque entr�e
        }
        // Attendre avant de changer de sc�ne
        yield return new WaitForSeconds(delayBeforeSceneChange);

        // Changer de sc�ne (� adapter selon votre configuration)
       // SceneManager.LoadScene("NomDeVotreScene");
    }


    IEnumerator FadeIn()
    {
        Debug.Log("fade lanc�");
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Attends pendant la dur�e d'affichage
        yield return new WaitForSeconds(2f);
    }
    IEnumerator FadeOut()
    {
        // Fondu de sortie
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        // D�sactive le popup       
    }
}