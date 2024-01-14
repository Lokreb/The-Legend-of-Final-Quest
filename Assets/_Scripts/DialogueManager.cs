using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public FadeOut fadeOut;
    public PlayerMapController playerMapController;
    public SuiveurCompagnon suiveurCompagnon;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel; // Référence au GameObject du panel de dialogue
    public string[] dialogues;

    public Button nextButton; // Référence au bouton pour passer au dialogue suivant

    void Start()
    {
        if (nextButton != null)
        {
            nextButton.interactable = false;
        }
    
        // Assurez-vous que le script de FadeOut est désactivé au début
        if (fadeOut != null)
        {
            fadeOut.enabled = false;
        }

        // Commencer le dialogue
        StartCoroutine(StartDialogue());
    }

    IEnumerator AfficherTexteProgressivement(string texte)
    {
        dialogueText.text = "";
        for (int i = 0; i < texte.Length; i++)
        {
            dialogueText.text += texte[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StartDialogue()
    {
        playerMapController.StartDialogue();
        suiveurCompagnon.StartDialogue();
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueText.text = dialogues[i];

            yield return StartCoroutine(AfficherTexteProgressivement(dialogues[i]));

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
                    playerMapController.EndDialogue();
                    suiveurCompagnon.EndDialogue();
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

            yield return new WaitForSeconds(2f); // Temps d'affichage de chaque dialogue
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