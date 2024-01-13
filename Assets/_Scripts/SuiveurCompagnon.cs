using System.Collections.Generic;   
using UnityEngine;

public class SuiveurCompagnon : MonoBehaviour
{
    public Transform joueur;
    public float distanceSuivi = 2f;
    public float vitesseDuCompagnon = 5f;

    private Vector3 dernierePositionJoueur;

    void Start()
    {
        dernierePositionJoueur = joueur.position;
    }

    void Update()
    {
        SuivreDeplacementsJoueur();
    }

    void SuivreDeplacementsJoueur()
    {
        // Obtient la direction actuelle du joueur
        Vector3 directionJoueur = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;

        // V�rifie si le joueur a chang� de direction
        if (directionJoueur != Vector3.zero)
        {
            // Calcul de la nouvelle position du compagnon en fonction de la direction oppos�e du joueur
            Vector3 nouvellePositionCompagnon = joueur.position - directionJoueur * distanceSuivi;

            // D�place le compagnon vers la nouvelle position
            transform.position = Vector3.MoveTowards(transform.position, nouvellePositionCompagnon, vitesseDuCompagnon * Time.deltaTime);

            // Met � jour la derni�re position du joueur
            dernierePositionJoueur = joueur.position;
        }
    }
}