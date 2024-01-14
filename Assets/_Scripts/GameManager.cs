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
    public int nbIntro;
    public Transform boss1;
    public Transform boss2;
    public static Vector3 savedPlayerPosition;
    public Vector3 passedPlayerPosition;

    int worldSceneIndex = 2;
    // Start is called before the first frame update
    void Awake()
    {
        partie = gameData.savedPartieIndex;
        question = gameData.savedQuestionIndex;
        boss = gameData.nbBoss;
        gender = gameData.playerGender;
        nbIntro = gameData.intro;
        if (SceneManager.GetActiveScene().buildIndex == worldSceneIndex)
        {
            Debug.Log(savedPlayerPosition);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (boss == 1)
            {
                Vector3 newPosition = new Vector3(boss1.position.x, boss1.position.y + 2.0f, boss1.position.z);
                player.transform.position = newPosition;
                passedPlayerPosition = player.transform.position;
            }
            else if (boss == 2)
            {
                Vector3 newPosition = new Vector3(boss2.position.x + 2.0f, boss2.position.y, boss2.position.z);
                player.transform.position = newPosition;
                passedPlayerPosition = player.transform.position;
            }
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

    public void saveGender(int gender)
    {
        gameData.playerGender = gender;
    }

    public void LoadGender()
    {
        gender = gameData.playerGender;
    }

    public void saveIntro(int introduction)
    {
        gameData.intro = introduction;
    }

    public void LoadIntro()
    {
        nbIntro = gameData.intro;
    }
}
