using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static Vector3 savedPlayerPosition;

    int worldSceneIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != worldSceneIndex)
        {
            // Charge la position sauvegardée du joueur
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = savedPlayerPosition;
        }
    }
}
