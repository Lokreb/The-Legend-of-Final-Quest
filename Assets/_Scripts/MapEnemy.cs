using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MapEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public UnityEvent onCollisionEnter2D; // Cr�ez cet �v�nement dans l'inspecteur Unity.


    
    // Cette fonction est appel�e lorsqu'il y a une collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision d�tect�e avec : " + other.gameObject.name);
       
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�a marche");
            onCollisionEnter2D.Invoke(); // D�clenche l'�v�nement Unity.
            Destroy(gameObject);
            //changer de scene
        }
        // Vous pouvez ajouter ici le code pour r�agir � la collision.
    }
}
