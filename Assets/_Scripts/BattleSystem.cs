using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Character_Stat playerstats;
    
    Enemy_stat enemystats;

     public TMP_Text lvl;
     public TMP_Text Enemyname;


        void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }
    void SetupBattle()
    {
         GameObject PlayerGO = Instantiate(playerPrefab);
         PlayerGO.GetComponent<Character_Stat>();
         GameObject EnemyGO = Instantiate(enemyPrefab);
         EnemyGO.GetComponent<Enemy_stat>();
        Enemyname.text = "" +EnemyGO.GetComponentInParent<Enemy_stat>().enemyName;
        lvl.text = ""+ PlayerGO.GetComponentInParent<Character_Stat>().level.ToString();
        
    }
}
