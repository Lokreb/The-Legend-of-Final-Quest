using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Character_Stat playerstats;
    Enemy_stat enemystats;

    public TextMeshPro lvl;
    public TextMeshPro Enemyname;

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
        lvl.text = playerstats.level.ToString();
    }
}
