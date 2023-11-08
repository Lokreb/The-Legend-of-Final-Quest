using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditorInternal;

public class PlayerMapController : MonoBehaviour
{

    public UnityEditor.Animations.AnimatorController[] ContollerL;
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    public LayerMask Walkable;
    public int gender = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //gender = le bon truc
        this.GetComponent<Animator>().runtimeAnimatorController = ContollerL[gender];
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Créez un vecteur de mouvement à partir des entrées du joueur.
        Vector2 movement = new Vector2(horizontal, vertical).normalized;


        // Maintenant, utilisez le vecteur de mouvement dans FixedUpdate.
        MovePlayer(movement);
    }

    private void MovePlayer(Vector2 movement)
    {
        // Calculer la position cible.
        Vector2 targetPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // Vérifier si la position cible est walkable.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, 0.2f, Walkable);

        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);

        if (colliders.Length > 0)
        {
            // Si la position cible est walkable, déplacer le personnage.
            rb.MovePosition(targetPosition);
        }
    }
}
