using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Character_Stat : MonoBehaviour
{
    public string gender;

    public int level = 1;
    public int damage;
    public int maxHealth;
    public int currentHealth;
    public List<Sprite> sprites;
   // public List<Animation> anims;
    public List<AnimatorController> Controllers;
    public int FinalDammage;
    SpriteRenderer spriteRenderer;
    public int dammagetaken;
    public int NBHeal = 3;
    int genderNB = 0;
    Animator animator;
    public GameManager _GM;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        AnimatorController controller = GetComponent<AnimatorController>();

        if (gender == "HOMME") 
        {
            genderNB = 0;
            spriteRenderer.sprite = sprites[genderNB];
        }
        else if(gender == "FEMME")
        {
            genderNB = 1;
            spriteRenderer.sprite = sprites[genderNB];
        }
        else if (gender == "NONE")
        {
            genderNB = 2;
            spriteRenderer.sprite = sprites[genderNB];
        }
        this.GetComponent<Animator>().runtimeAnimatorController = Controllers[genderNB];

        damage = 10;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

   public void CalculedDammage()
    {
        FinalDammage = level * 2 ^ level * damage;
        animator.SetFloat("isAttak", 1);
        animator.SetFloat("isAttak", 0);
    }

    public bool TakeDamage(int dammage,int trueDammage)
    {
       
        animator.SetFloat("isTanking", 1);
        currentHealth = currentHealth - trueDammage;
        //affichage de faux dommage 
        animator.SetFloat("isTanking", 0);
        if (currentHealth <= 0)
        {
            return true;
        }
        else { return false; }
    }

    public void heal()
    {
        if(currentHealth < maxHealth && NBHeal > 0)
        {
            animator.SetFloat("isAttak", -1);
            currentHealth = maxHealth;
            NBHeal--;
            animator.SetFloat("isAttak", 0);
        }
        
        
            
        
        
    }
}
    

