using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Stat : MonoBehaviour
{
    public string gender;

    public int level = 1;
    public int damage;
    public int maxHealth;
    public int currentHealth;
    public List<Sprite> sprites;
    public int FinalDammage;
    SpriteRenderer spriteRenderer;
    public int dammagetaken;
    public int NBHeal = 3;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gender == "HOMME") 
        {
        spriteRenderer.sprite = sprites[0];
        }
        else if(gender == "FEMME")
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (gender == "NONE")
        {
            spriteRenderer.sprite = sprites[2];
        }
        
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
        if(currentHealth < maxHealth && NBHeal > 0)
        {
            currentHealth = maxHealth;
            NBHeal--;
        }
        
        
            
        
        
    }
}
    

