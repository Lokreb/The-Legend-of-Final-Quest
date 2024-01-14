using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 3.0f; // Durée en secondes pour la disparition complète

    private SpriteRenderer spriteRenderer;
    private float currentFadeTime = 0.0f;

    void Start()
    {
        // Assurez-vous que le sprite renderer est attaché à l'objet
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Incrémentez le temps écoulé
        currentFadeTime += Time.deltaTime;

        // Calculez le pourcentage de transparence en fonction du temps écoulé
        float alphaPercentage = 1.0f - (currentFadeTime / fadeDuration);

        // Limitez la valeur alpha entre 0 et 1
        alphaPercentage = Mathf.Clamp01(alphaPercentage);

        // Mettez à jour l'opacité du sprite
        Color newColor = spriteRenderer.color;
        newColor.a = alphaPercentage;
        spriteRenderer.color = newColor;
    }
}
