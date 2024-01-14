using System.Collections.Generic;   
using UnityEngine;

public class SuiveurCompagnon : MonoBehaviour
{
    public Transform joueur;
    public float distanceSuivi = 2f;
    public float vitesseDuCompagnon = 5f;

    private Vector3 dernierePositionJoueur;
    private bool isDialogueActive = false; // Ajout de la variable pour indiquer si un dialogue est en cours

    void Start()
    {
        dernierePositionJoueur = joueur.position;
    }

    void Update()
    {
        if (!isDialogueActive) // Vérifier si le dialogue est actif avant de permettre le suivi
        {
            SuivreDeplacementsJoueur();
        }
    }

    void SuivreDeplacementsJoueur()
    {
        // Obtient la direction actuelle du joueur
        Vector3 directionJoueur = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;

        // Vérifie si le joueur a changé de direction
        if (directionJoueur != Vector3.zero)
        {
            // Calcul de la nouvelle position du compagnon en fonction de la direction opposée du joueur
            Vector3 nouvellePositionCompagnon = joueur.position - directionJoueur * distanceSuivi;

            // Déplace le compagnon vers la nouvelle position
            transform.position = Vector3.MoveTowards(transform.position, nouvellePositionCompagnon, vitesseDuCompagnon * Time.deltaTime);

            // Met à jour la dernière position du joueur
            dernierePositionJoueur = joueur.position;
        }
    }

    // Ajout de méthodes pour activer/désactiver le dialogue
    public void StartDialogue()
    {
        isDialogueActive = true;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
    }
}