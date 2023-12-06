using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int lives = 10; // Vous pouvez ajuster ce nombre selon les besoins du jeu
    private int gemstoneCount = 0;
    public float moveSpeed = 5.0f;
    public Animator animator;
    public float attackRange = 1.0f; // Portée de l'attaque
    public LayerMask enemyLayers; // Couches (layers) des ennemis

    private Vector2 movement;
    private bool isAttacking = false;

    public int health = 100; // Points de vie du joueur

    public UIManager uiManager; // Ajoutez cette référence

    public GameController gameController; // Ajoutez cette référence

    public void CollectGemstone()
    {
        gemstoneCount++; // Incrémente le compteur de pierres

        if (uiManager != null)
        {
            uiManager.UpdateGemstoneCount(gemstoneCount); // Met à jour l'UI
        }
    }

    public int GetGemstoneCount()
    {
        return gemstoneCount; // Retourne le nombre de pierres collectées
    }

    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateGemstoneCount(gemstoneCount);
            // Ajoutez ici d'autres mises à jour d'interface si nécessaire
        }
    }


    void Update()
    {
        if (!gameController.isGameActive) return; // Ajoutez cette ligne pour vérifier l'état du jeu

        // Gestion des entrées pour le mouvement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Vérifier si le joueur commence une attaque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            Attack();
        }
        else if (isAttacking)
        {
            // Réinitialiser l'état d'attaque après l'animation
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackAnimation")) // Remplacez par le nom réel de votre animation d'attaque
            {
                isAttacking = false;
                animator.SetBool("isAttacking", false);
            }
        }

        UpdateAnimator();
    }

    void FixedUpdate()
    {
        // Déplacement du joueur
        MovePlayer(movement);
    }

    void MovePlayer(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        // Détecter les ennemis dans la portée de l'attaque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        // Infliger des dégâts à ces ennemis
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit: " + enemy.name);
            // Ici, ajoutez la logique pour gérer les dégâts sur l'ennemi
            // Par exemple, appeler une méthode sur un script EnemyController attaché à l'ennemi
        }
    }

    void UpdateAnimator()
    {
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetBool("isMoving", movement.sqrMagnitude > 0);
        animator.SetFloat("AttackingDirectionX", movement.x);
        animator.SetFloat("AttackingDirectionY", movement.y);
    }

    void OnDrawGizmosSelected()
    {
        // Aide visuelle pour voir la portée de l'attaque dans l'éditeur
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        // Ajoutez une condition pour vérifier si le jeu est actif
        if (!gameController.isGameActive) return;

        lives -= damage;
        health -= damage;

        if (lives <= 0 || health <= 0)
        {
            Die();
        }

        if (uiManager != null)
        {
            uiManager.UpdateLivesCount(lives); // Met à jour l'UI
        }
    }

    public int GetLives()
    {
        return lives;
    }



    void Die()
    {
        // Logique de mort du joueur - par exemple, fin du jeu
        Debug.Log("Player died!");
        gameController.EndGame(); // Supposons que vous ayez une référence à GameController
    }
}
