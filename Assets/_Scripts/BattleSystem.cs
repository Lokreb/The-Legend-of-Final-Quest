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
    public GameObject QuestionManagerGO;
    public Button[] questionBouton;
    public bool isPlayerTurn;
     public TMP_Text lvl;
     public TMP_Text Enemyname;
     public Slider EnemyHPBar;
     public Slider PlayerHPBar;


    public Enemy_stat enemy_unit;
    public Character_Stat player_unit;
        void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if(state == BattleState.PLAYERTURN)
        {
            foreach(Button bouton in questionBouton)
            {
                bouton.interactable = true;
            }
        } else
        {
            foreach (Button bouton in questionBouton)
            {
                bouton.interactable = false;
            }
        }
    }

    IEnumerator SetupBattle()
    {
        QuestionManagerGO.GetComponent<QuestionManager>();
         GameObject PlayerGO = Instantiate(playerPrefab);
        player_unit =  PlayerGO.GetComponent<Character_Stat>();
         GameObject EnemyGO = Instantiate(enemyPrefab);
         enemy_unit = EnemyGO.GetComponent<Enemy_stat>();
        Enemyname.text = "" + enemy_unit.enemyName;
        lvl.text = ""+ player_unit.level.ToString();
        enemy_unit.NBQuestion = QuestionManagerGO.GetComponent<QuestionManager>().NbQuestion;
        enemy_unit.initiallisationHP();
        EnemyHPBar.maxValue = enemy_unit.MaxHealth;
        PlayerHPBar.maxValue = player_unit.maxHealth;

        
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        player_unit.CalculedDammage();
        bool isdead = enemy_unit.TakeDamage(player_unit.FinalDammage);
        EnemyHPBar.value = enemy_unit.currentHealth;
        yield return new WaitForSeconds(2f);

        if(isdead)
        {
            EndBattle();
            state = BattleState.WON;
        }else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        IEnumerator EnemyTurn()
        {
            Debug.Log("tours enemmie");
            yield return new WaitForSeconds(1f);
            bool isDead = player_unit.TakeDamage(enemy_unit.damage,enemy_unit.TrueDammage);
            PlayerHPBar.value = player_unit.currentHealth;
            yield return new WaitForSeconds(1f);

            if(isdead)
            {
                
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }

        void EndBattle()
        {
            if (state == BattleState.WON)
            {
                Debug.Log("tu a gagner");
            }
            else if (state == BattleState.LOST)
            {
                Debug.Log("tu a perdu");
            } 

        }


        //chek if question is end
        //change state based on that
    }
}
