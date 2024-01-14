using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom/GameData", order = 1)]
public class GameData : ScriptableObject
{

    public static Vector3 savedPlayerPosition;
    public int savedPartieIndex;
    public int savedQuestionIndex;
    public int nbBoss;
    public int playerGender;
    public int intro;

}
