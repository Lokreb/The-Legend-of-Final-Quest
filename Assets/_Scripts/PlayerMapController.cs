using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditorInternal;

public class PlayerMapController : MonoBehaviour
{

    public UnityEditor.Animations.AnimatorController[] ContollerL;
    public GameManager GM;
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDialogueActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        animator = GetComponent<Animator>();
        this.GetComponent<Animator>().runtimeAnimatorController = ContollerL[GM.gender];
    }

    private void Update()
    {
        if (!isDialogueActive)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Créez un vecteur de mouvement à partir des entrées du joueur.
            Vector2 movement = new Vector2(horizontal, vertical);

            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
            MovePlayer(movement);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Collision Mur");
        }
    }

    private void MovePlayer(Vector2 movement)
    {
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);
    }

    public void StartDialogue()
    {
        isDialogueActive = true;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
    }
}
