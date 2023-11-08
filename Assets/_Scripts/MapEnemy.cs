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
    public UnityEvent onCollisionEnter2D; // Créez cet événement dans l'inspecteur Unity.


    
    // Cette fonction est appelée lorsqu'il y a une collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision détectée avec : " + other.gameObject.name);
       
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ça marche");
            onCollisionEnter2D.Invoke(); // Déclenche l'événement Unity.
            Destroy(gameObject);
            //changer de scene
        }
        // Vous pouvez ajouter ici le code pour réagir à la collision.
    }
}
