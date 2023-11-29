using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameData gameData;
    public int partie;
    public int question;
    public int boss;
    public static Vector3 savedPlayerPosition;

    int worldSceneIndex = 1;
    // Start is called before the first frame update
    void Awake()
    {
        partie = gameData.savedPartieIndex;
        question = gameData.savedQuestionIndex;
        boss = gameData.nbBoss;
        if (SceneManager.GetActiveScene().buildIndex != worldSceneIndex)
        {
            // Charge la position sauvegardée du joueur
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //player.transform.position = savedPlayerPosition;
            Vector3 newPosition = new Vector3(savedPlayerPosition.x, savedPlayerPosition.y + 1.0f, savedPlayerPosition.z);
            player.transform.position = newPosition;
        }
    }

    public void SavePartie(int part) {
        gameData.savedPartieIndex = part;
    }

    public void SaveQuestion(int quest) {
        gameData.savedPartieIndex = quest;
    }

    public void SaveBoss(int numBoss) {
        gameData.nbBoss = numBoss;
    }
}
