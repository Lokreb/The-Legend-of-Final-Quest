using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*public class MapEnemy : MonoBehaviour
{
    Vector3 savedPlayerPosition;
    public int whoAmI = 0;
    private QuestionManager QM;
    private int indexPartie;
    public UnityEvent onCollisionEnter2D; // Cr�ez cet �v�nement dans l'inspecteur Unity.
    public string LevelToLoad;


    void Start()
    {
        if(QM.currentPartieIndex > whoAmI) {
            Destroy(gameObject);
        }
    }

    void LoadLevel()
    {
        
        SceneManager.LoadScene(LevelToLoad);
    }

    // Cette fonction est appel�e lorsqu'il y a une collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision d�tect�e avec : " + other.gameObject.name);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameManager.savedPlayerPosition = player.transform.position; // Sauvegarde la position du joueur dans GameManager
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�a marche");
            onCollisionEnter2D.Invoke(); // D�clenche l'�v�nement Unity.
            Destroy(gameObject);
            LoadLevel();
        }
        // Vous pouvez ajouter ici le code pour r�agir � la collision.
    }
}*/

public class MapEnemy : MonoBehaviour
{
    public int whoAmI = 0;
    public UnityEvent onCollisionEnter2D;
    public string LevelToLoad;
    public GameManager GM;

    void Update()
    {
        // Récupère l'index de la partie actuelle sauvegardée
        
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
