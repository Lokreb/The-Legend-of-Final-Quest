using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimPanzo : MonoBehaviour
{
    private Animator animator;
    private int loopCount = 0;
    private bool isAnimationPlaying = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Loop", loopCount);
    }

    void Update()
    {
        if (!isAnimationPlaying)
        {
            // Votre logique de détection du début de l'animation
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                isAnimationPlaying = true;
            }
        }
        else
        {
            // Votre logique de détection de la fin de l'animation
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                // Incrémentez le compteur de boucles
                loopCount++;

                // Mettez à jour le paramètre dans l'Animator
                animator.SetInteger("Loop", loopCount);

                // Réinitialisez le compteur et le drapeau si nécessaire
                if (loopCount >= 3)
                {
                    loopCount = 0;
                    animator.SetInteger("Loop", loopCount);
                    isAnimationPlaying = false;
                }
            }
        }
    }
}
