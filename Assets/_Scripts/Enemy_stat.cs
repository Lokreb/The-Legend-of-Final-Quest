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

    void Start()
    {
        
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

    public bool TakeDamage(int dammage)
    {

        currentHealth = currentHealth - 1;
        //affichage de faux dommage 
        if (currentHealth <= 0)
        {
            return true;
        }
        else { return false; }
    }
}
