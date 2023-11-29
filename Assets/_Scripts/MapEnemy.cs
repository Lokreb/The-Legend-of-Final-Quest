using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapEnemy : MonoBehaviour
{
    public int whoAmI = 0;
    public UnityEvent onCollisionEnter2D;
    public string LevelToLoad;
    public GameManager GM;

    void Update()
    {
        // Vérifie si cet ennemi correspond à la progression actuelle du questionnaire
        if (GM.partie > whoAmI)
        {
            Destroy(gameObject); // Détruit l'ennemi car la partie actuelle est plus avancée
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Sauvegarde la position du joueur dans le GameManager s'il est utilisé pour sauvegarder des données
            GameManager.savedPlayerPosition = other.transform.position;

            onCollisionEnter2D.Invoke(); // Déclenche l'événement Unity.
            Destroy(gameObject); // Détruit l'ennemi

            // Charge la scène associée au combat ou à la suite du jeu
            SceneManager.LoadScene(LevelToLoad);
        }
    }
}
