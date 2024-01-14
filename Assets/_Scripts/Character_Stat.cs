using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Character_Stat : MonoBehaviour
{
    public int level = 1;
    public int damage;
    public int maxHealth;
    public int currentHealth;
    public List<Sprite> sprites;
   // public List<Animation> anims;
    public RuntimeAnimatorController[] Controllers;
    public int FinalDammage;
    public int dammagetaken;
    public int NBHeal = 3;
    int genderNB = 0;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        RuntimeAnimatorController controller = GetComponent<RuntimeAnimatorController>();
        //this.GetComponent<Animator>().runtimeAnimatorController = Controllers[genderNB];

        damage = 10;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

   public void CalculedDammage()
    {
        FinalDammage = level * 2 ^ level * damage;  
    }

    public bool TakeDamage(int dammage,int trueDammage)
    {
       
        animator.SetFloat("isTanking", 1);
        currentHealth = currentHealth - trueDammage;
        //affichage de faux dommage 
        
        if (currentHealth <= 0)
        {
            return true;
        }
        else { return false; }
    }

    public void heal()
    {
        if (currentHealth < maxHealth && NBHeal > 0)
        {
            animator.SetFloat("isAttak", -1);
            currentHealth = maxHealth;
            NBHeal--;

        }
    } 
    public void GenderChange(int genre)
    {
        this.GetComponent<Animator>().runtimeAnimatorController = Controllers[genre];
    }  
            
        
        
    
}
    

