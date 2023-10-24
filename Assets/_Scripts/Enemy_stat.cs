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

    void Start()
    {
        NBQuestion = 3;
        MaxHealth = NBQuestion;
        currentHealth = MaxHealth;
        TrueDammage = 10;
        damage = 10 * 2 ^ currentPart * 20;
    }
}
