using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float attackRange = 1.0f;
    public LayerMask playerLayer;

    private Transform playerTransform;
    public Animator animator;

    public GameController gameController;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!gameController.isGameActive) return; // Ne fait rien si le jeu n'est pas actif

        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Mettre à jour les paramètres d'animation
        animator.SetFloat("horizontal", direction.x);
        animator.SetFloat("vertical", direction.y);
        animator.SetBool("isMoving", direction.magnitude > 0);

        if (Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            AttackPlayer();
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PlayerController playerController = playerTransform.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            animator.SetBool("isDying", true);
            gameController.DecrementEnemyCount();
            Destroy(gameObject, 1f); // Permet à l'animation de mort de se jouer
        }
    }
}
