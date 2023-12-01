using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.0f; // La portée de l'attaque
    public int attackDamage = 1; // Les dégâts infligés par l'attaque
    public float attackCooldown = 2.0f; // Le temps de recharge entre les attaques

    private float lastAttackTime = 0; // Quand la dernière attaque a eu lieu

    void Start()
    {
        lastAttackTime = -attackCooldown; // Initialise pour permettre une attaque immédiate
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Player"));

            foreach (var hitPlayer in hitPlayers)
            {
                if (hitPlayer.gameObject.CompareTag("Player"))
                {
                    // Inflige des dégâts au joueur
                    hitPlayer.GetComponent<Movement>().TakeDamage(attackDamage);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Afficher la portée de l'attaque dans l'éditeur
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
