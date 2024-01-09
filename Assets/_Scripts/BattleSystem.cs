using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameManager GM;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefab;
    public GameObject QuestionManagerGO;
    public Button[] questionBouton;
    public Button[] ActionButton;
    public bool isPlayerTurn;
    public TMP_Text lvl;
    public TMP_Text Enemyname;
    public TMP_Text Nbheal;
    public Slider EnemyHPBar;
    public Slider PlayerHPBar;
    public Button[] AttakButton;
    public int AttakType;
    public GameObject[] QuestionPanel;
    public GameObject ActionPanel;
    public GameObject AttaquePanel;
    public Enemy_stat enemy_unit;
    public Character_Stat player_unit;
    public GameObject EcranDeChargement;
    public string LevelToLoad;
    // public bool Repondu = false;
    public WhereIamI Wii;
    public QuestionManager _QM;
    void Start()
    {
        EcranDeChargement.SetActive(true);
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            // Utilisez musicManager pour contrôler la musique
        }
        state = BattleState.START;
        StartCoroutine(SetupBattle());
            
       
    }

    private void Update()
    {

        if (state == BattleState.PLAYERTURN)
        {
            foreach (Button bouton in ActionButton)
            {
                bouton.interactable = true;
            }
        }
        else if (state != BattleState.PLAYERTURN)
        {
            foreach (Button bouton in ActionButton)
            {
                bouton.interactable = false;
            }
        }
        Nbheal.text = "Restant : " + player_unit.NBHeal.ToString();

    }

    IEnumerator SetupBattle()
    {


        QuestionManagerGO.GetComponent<QuestionManager>();
        GameObject PlayerGO = Instantiate(playerPrefab);
        player_unit = PlayerGO.GetComponent<Character_Stat>();
        GameObject EnemyGO = Instantiate(enemyPrefab[Wii.CounterOfBoss]);
        enemy_unit = EnemyGO.GetComponent<Enemy_stat>();
        Enemyname.text = "" + enemy_unit.enemyName;
        player_unit.level = GM.partie + 1;
        lvl.text = "" + player_unit.level.ToString();
        yield return new WaitForSeconds(1f);
        enemy_unit.NBQuestion = QuestionManagerGO.GetComponent<QuestionManager>().NbQuestion;
        enemy_unit.initiallisationHP();
        EnemyHPBar.maxValue = enemy_unit.MaxHealth;
        PlayerHPBar.maxValue = player_unit.maxHealth;
        player_unit.NBHeal = 3;
        enemy_unit.weakness = Random.Range(1, 4);
        DisablePanels();
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(1f);
        EcranDeChargement.SetActive(false);
        PlayerTurn();
    }

    void DisablePanels()
    {
        foreach (GameObject qp in QuestionPanel)
        {
            qp.SetActive(false);
        }
    }

    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        ActionPanel.SetActive(true);
        AttaquePanel.SetActive(false);
        Debug.Log("Player Turn");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;
        if (AttakType == 5)
        {
            player_unit.heal();

        }
        StartCoroutine(PlayerAttack());

    }

    public void DefineAttakType1()
    {
        AttakType = 1;
        player_unit.animator.SetFloat("AtkType", AttakType);
    }
    public void DefineAttakType2()
    {
        AttakType = 2;
        player_unit.animator.SetFloat("AtkType", AttakType);
    }
    public void DefineAttakType3()
    {
        AttakType = 3;
        player_unit.animator.SetFloat("AtkType", AttakType);
    }
    public void DefineAttakType4()
    {
        AttakType = 4;
        player_unit.animator.SetFloat("AtkType", AttakType);
    }
    public void DefineAttakType5()
    {
        AttakType = 5;
       // player_unit.animator.SetFloat(isAttak, AttakType);
    }

    IEnumerator PlayerAttack()
    {
        player_unit.CalculedDammage();
        if (AttakType != enemy_unit.weakness && AttakType != 5)
        {
            enemy_unit.Hit();
            enemy_unit.animator.SetFloat("isAttak", 1);
            yield return new WaitForSeconds(3.250f);
            enemy_unit.animator.SetFloat("isAttak", 0);
            player_unit.animator.SetFloat("isTanking", 1);
            bool isdead = player_unit.TakeDamage(enemy_unit.damage, enemy_unit.TrueDammage);
            PlayerHPBar.value = player_unit.currentHealth;           
            player_unit.animator.SetFloat("isTanking", 0);
            

        }
        else
        {
          //todo 
        }
        if (AttakType != 5) //this is not a heal
        {
            Reponse();

            while (!QuestionManagerGO.GetComponent<QuestionManager>().Repondu)
            {
                yield return null; // Attendez la prochaine frame
            }
            Reponse();
            QuestionManagerGO.GetComponent<QuestionManager>().Repondu = false;
            player_unit.animator.SetFloat("isAttak", 1);
            yield return new WaitForSeconds(2.4f);
            player_unit.animator.SetFloat("isAttak", 0);
            bool isdead = enemy_unit.TakeDamage(player_unit.FinalDammage);
            EnemyHPBar.value = enemy_unit.currentHealth;
            enemy_unit.animator.SetFloat("isTanking", 1);
            yield return new WaitForSeconds(1.04f);
            enemy_unit.animator.SetFloat("isTanking", 0);
            if (isdead)
            {
                state = BattleState.WON;
                enemy_unit.animator.SetFloat("isTanking", -1);
                enemy_unit.animator.SetFloat("isAttak", -1);
                yield return new WaitForSeconds(1.7f);
                enemy_unit.animator.SetFloat("isTanking", 0);
                enemy_unit.animator.SetFloat("isAttak", 0);
                EndBattle();

            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }



        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());

        }

        IEnumerator EnemyTurn()
        {
            Debug.Log("tours enemmie");
            enemy_unit.Hit();
            enemy_unit.animator.SetFloat("isAttak", 1);
            yield return new WaitForSeconds(3.250f);
            enemy_unit.animator.SetFloat("isAttak", 0);
            bool isdead = player_unit.TakeDamage(enemy_unit.damage, enemy_unit.TrueDammage);
            PlayerHPBar.value = player_unit.currentHealth;
            yield return new WaitForSeconds(1.04f);
            player_unit.animator.SetFloat("isTanking", 0);

            if (isdead)
            {
                player_unit.animator.SetFloat("isTanking", -1);
                player_unit.animator.SetFloat("isAttak", -1);
                yield return new WaitForSeconds(1f);
                player_unit.animator.SetFloat("isTanking", 0);
                player_unit.animator.SetFloat("isAttak", 0);
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        //Aodhan was here
        void EndBattle()
        {
            if (state == BattleState.WON)
            {
                Debug.Log("tu as gagner");
                player_unit.level++;
                Wii.CounterOfBoss++;
                Debug.Log("Je sauvegarde : " + QuestionManagerGO.GetComponent<QuestionManager>().currentPartieIndex);
                GM.SaveBoss(Wii.CounterOfBoss);
                EcranDeChargement.SetActive(true);
                LoadLevel();
            }
            else if (state == BattleState.LOST)
            {
                Debug.Log("tu as perdu");
            }

        }
        void LoadLevel()
        {
            SceneManager.LoadScene(LevelToLoad);
        }


        //chek if question is end
        //change state based on that
    }

    public void Reponse()
    {
        if (QuestionManagerGO.GetComponent<QuestionManager>().Repondu == false)
        {
            AttaquePanel.SetActive(false);
            if(QuestionManagerGO.GetComponent<QuestionManager>().questionType == 1)
            {
                QuestionPanel[0].SetActive(true);
            } 
            else if (QuestionManagerGO.GetComponent<QuestionManager>().questionType == 2)
            {
                QuestionPanel[1].SetActive(true);
            }
            else if (QuestionManagerGO.GetComponent<QuestionManager>().questionType == 3)
            {
                QuestionPanel[2].SetActive(true);
            }
        }
        else if (QuestionManagerGO.GetComponent<QuestionManager>().Repondu == true)
        {
            //QuestionPanel.SetActive(false);
            for (int i = 0; i < QuestionPanel.Length; i++)
            {
                QuestionPanel[i].SetActive(false);
            }
            AttaquePanel.SetActive(true);
        }


    }
}
