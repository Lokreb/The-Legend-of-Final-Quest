using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Stat : MonoBehaviour
{
    public string gender;

    public int level;
    public int damage;
    public int maxHealth;
    public int currentHealth;
    public List<Sprite> sprites;
    public double FinalDammage;
    SpriteRenderer spriteRenderer;
    

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
        level = 1;
        damage = 10;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    void CalculedDammage()
    {
        FinalDammage = level * 2 ^ level * damage;
    }

    
}
