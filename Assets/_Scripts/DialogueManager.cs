using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameManager GM;
    public FadeOut fadeOut;
    public PlayerMapController playerMapController;
    public SuiveurCompagnon suiveurCompagnon;
    private bool isLastCharDisplayed;
    public GameObject menuButton;
    public Animator animator;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    [TextAreaAttribute]
    public string[] dialogues;
    public float displaySpeed = 1f;
    public Button nextButton; // Référence au bouton pour passer au dialogue suivant

    void Start()
    {
        if(GM.nbIntro != 1)
        {
            dialoguePanel.SetActive(true);
            if (nextButton != null)
            {
                nextButton.interactable = false;
            }
            if (fadeOut != null)
            {
                fadeOut.enabled = false;
            }

            // Commencer le dialogue
            StartCoroutine(StartDialogue());
        }
        //DontDestroyOnLoad(gameObject);
    }

    IEnumerator AfficherTexteProgressivement(string texte, float speed)
    {
        dialogueText.text = "";
        for (int i = 0; i < texte.Length; i++)
        {
            dialogueText.text += texte[i];
            isLastCharDisplayed = i == texte.Length - 1;
            yield return new WaitForSeconds(speed);
        }
    }

    IEnumerator StartDialogue()
    {
        menuButton.SetActive(false);
        playerMapController.StartDialogue();
        suiveurCompagnon.StartDialogue();
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueText.text = dialogues[i];

            if (animator != null)
            {
                animator.SetBool("isTalking", true);
            }

            yield return StartCoroutine(AfficherTexteProgressivement(dialogues[i], displaySpeed));

            if (animator != null && isLastCharDisplayed)
            {
                animator.SetBool("isTalking", false);
                Debug.Log("Derniere lettre");
            }

            yield return WaitForButtonPress();


            // Désactiver le bouton après le dernier dialogue
            if (i == dialogues.Length - 1)
            {
                if (nextButton != null)
                {
                    nextButton.interactable = false;
                }

                // Désactiver le panel de dialogue
                if (dialoguePanel != null)
                {
                    GM.saveIntro(1);
                    GM.LoadIntro();
                    playerMapController.EndDialogue();
                    suiveurCompagnon.EndDialogue();
                    menuButton.SetActive(true);
                    dialoguePanel.SetActive(false);
                }
            }

            // Déclencher le FadeOut après le deuxième dialogue
            if (i == 1)
            {
                if (fadeOut != null)
                {
                    // Activer le script de FadeOut après le deuxième dialogue
                    fadeOut.enabled = true;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator WaitForButtonPress()
{
        bool buttonPressed = false;

        // Activer le bouton
        if (nextButton != null)
        {
            nextButton.interactable = true;
        }

        // Attendre que le bouton soit pressé ou que la touche Entrée soit enfoncée
        while (!buttonPressed)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                buttonPressed = true;
            }

            yield return null;
        }

        // Désactiver le bouton après qu'il a été pressé
        if (nextButton != null)
        {
            nextButton.interactable = false;
        }

        // Attendre un court laps de temps pour éviter la détection multiple du bouton
        yield return new WaitForSeconds(0.1f);
    }
}