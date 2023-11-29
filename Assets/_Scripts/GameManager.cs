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
    public int gender;
    public static Vector3 savedPlayerPosition;
    private static bool isGMcreated = false;

    int worldSceneIndex = 1;
    // Start is called before the first frame update
    void Awake()
    {
        if(!isGMcreated) {
            DontDestroyOnLoad(gameObject);
            isGMcreated = true;
        } else {
            Destroy(gameObject);
        }
        partie = gameData.savedPartieIndex;
        question = gameData.savedQuestionIndex;
        boss = gameData.nbBoss;
        if (SceneManager.GetActiveScene().buildIndex != worldSceneIndex)
        {
            // Charge la position sauvegardée du joueur
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(boss ==1) {
                Vector3 newPosition = new Vector3(savedPlayerPosition.x, savedPlayerPosition.y + 1.0f, savedPlayerPosition.z);
                player.transform.position = newPosition;
            } else if (boss == 2) {
                Vector3 newPosition = new Vector3(savedPlayerPosition.x + 1.0f, savedPlayerPosition.y, savedPlayerPosition.z);
                player.transform.position = newPosition;
            }
        }
    }

    void Start() {
        gameData.ResetData();
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

    public void SaveGender(int gend) {
        gameData.gender = gend;
    }


}
