using UnityEngine;
using TMPro;
using System.Collections;

public class TexteAffichage : MonoBehaviour
{
    public float vitesseApparition = 0.1f; 
    private TMP_Text texteTMP;
    private string texteComplet;

    void Start()
    {
        texteTMP = GetComponent<TMP_Text>();
        texteComplet = texteTMP.text;
        texteTMP.text = ""; 

        StartCoroutine(AfficherTexteProgressivement());
    }

    IEnumerator AfficherTexteProgressivement()
    {
        for (int i = 0; i < texteComplet.Length; i++)
        {
            texteTMP.text += texteComplet[i];
            yield return new WaitForSeconds(vitesseApparition);
        }
    }
}