using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int savedPartieIndex;
    public int savedQuestionIndex;
    public int nbBoss;
    public int gender;


    public void ResetData()
    {
        savedPartieIndex = 0;
        savedQuestionIndex = 0;
        nbBoss = 0;
        gender = 2;
    }

}
