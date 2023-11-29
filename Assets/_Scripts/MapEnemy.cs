using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapEnemy : MonoBehaviour
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
}
