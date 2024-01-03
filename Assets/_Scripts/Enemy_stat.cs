using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_stat : MonoBehaviour
{
    public string enemyName;
    public int MaxHealth;
    public int currentHealth;
    public int NBQuestion;
    public int damage;
    public int TrueDammage;
    public int currentPart;
    public int weakness = 1;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealth = NBQuestion;
        currentHealth = MaxHealth;
        TrueDammage = 10;
        damage = 10 * 2 ^ currentPart * 20;
        
    }

    public void initiallisationHP()
    {
        MaxHealth = NBQuestion;
        currentHealth = MaxHealth;
    }

    public void Hit()
    {
     
        animator.SetFloat("isAttak", 1);
        
    }
    public bool TakeDamage(int dammage)
    {

        currentHealth = currentHealth - 1;
        //affichage de faux dommage 
        if (currentHealth <= 0)
        {
            Debug.Log("mort");
            return true;
        }
        else {
            animator.SetFloat("isTanking", 1);
            Debug.Log("pas mort");
            
            return false; }
    }
}
