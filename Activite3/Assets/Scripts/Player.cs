using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 2;
    public Animator animator;
    private Vector3 direction;
    public Inventory inventory; // Assurez-vous que cette référence est correctement établie
    public GameObject gameOverScreen; // Assigner l'objet de l'interface utilisateur de fin de jeu
    public int health = 10; // Santé du joueur

    private Vector2 lastMoveDirection = Vector2.zero; // Dernière direction de mouvement

    void Awake()
    {
        // Vérifier et récupérer le composant Inventory si nécessaire
        if (inventory == null)
        {
            Player_Inventory playerInventory = GetComponent<Player_Inventory>();
            if (playerInventory != null)
            {
                inventory = playerInventory.inventory;
            }
            else
            {
                Debug.LogError("Le composant Player_Inventory est introuvable sur le Player.");
            }
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical, 0).normalized;
        AnimateMovement(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.Space)) // Ou une autre touche pour attaquer
        {
            PerformAttack();
        }

        if (direction.magnitude > 0)
        {
            lastMoveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    void AnimateMovement(float horizontal, float vertical)
    {
        if (animator != null)
        {
            bool isMoving = horizontal != 0 || vertical != 0;
            animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                animator.SetFloat("horizontal", horizontal);
                animator.SetFloat("vertical", vertical);
            }
        }
        else
        {
            Debug.LogError("Aucun Animator trouvé sur le Player.");
        }
    }

    void PerformAttack()
    {
        float attackDirectionX, attackDirectionY;

        if (direction.magnitude > 0) // Le personnage est en mouvement
        {
            attackDirectionX = direction.x;
            attackDirectionY = direction.y;
        }
        else // Le personnage est immobile
        {
            // Utilisez la dernière direction de mouvement connue pour déterminer la direction d'attaque
            if (lastMoveDirection != Vector2.zero)
            {
                attackDirectionX = lastMoveDirection.x;
                attackDirectionY = lastMoveDirection.y;
            }
            else
            {
                // Si lastMoveDirection est (0, 0), utilisez une direction par défaut, par exemple vers le bas
                attackDirectionX = 0;
                attackDirectionY = -1;
            }
        }

        // Mettez à jour l'Animator
        animator.SetBool("isAttacking", true);
        animator.SetFloat("AttackDirectionX", attackDirectionX);
        animator.SetFloat("AttackDirectionY", attackDirectionY);

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        // Attendez la fin de l'animation d'attaque ou un délai fixe
        yield return new WaitForSeconds(1.0f); // Ajustez ce délai en fonction de la durée de votre animation d'attaque

        // Réinitialisez le paramètre d'attaque
        animator.SetBool("isAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void LosePoints()
    {
        if (inventory != null && inventory.slots.Count > 0)
        {
            RemoveItemsFromInventory(1);

            if (IsInventoryEmpty())
            {
                Die();
            }
        }
    }


    void RemoveItemsFromInventory(int itemCount)
    {
        foreach (var slot in inventory.slots)
        {
            if (slot.count > 0)
            {
                slot.count -= itemCount;
                if (slot.count <= 0) slot.type = Collectable.CollectableType.NONE;
                break;
            }
        }
    }

    bool IsInventoryEmpty()
    {
        foreach (var slot in inventory.slots)
        {
            if (slot.count > 0)
            {
                return false;
            }
        }
        return true;
    }

    void Die()
    {
        this.enabled = false;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("Game Over Screen n'est pas assigné dans l'inspecteur!");
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharge la scène actuelle
    }
}
