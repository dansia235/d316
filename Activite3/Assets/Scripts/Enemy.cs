using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 5.0f;
    public float moveSpeed = 2.0f;
    private GameObject player;
    private Player playerMovementScript; // Référence au script Movement
    private int health = 2; // Santé de l'ennemi
    private Animator animator; // Variable pour l'Animator

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovementScript = player.GetComponent<Player>(); // Récupération du script Movement
        animator = GetComponent<Animator>(); // Récupération du composant Animator
        InvokeRepeating("SpawnRandomly", 0.0f, 10.0f); // Spawns the enemy randomly every 10 seconds
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
            animator.SetBool("isWalking", true); // Active l'animation de marche
        }
        else
        {
            animator.SetBool("isWalking", false); // Désactive l'animation de marche
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerMovementScript.LosePoints(); // Appelle LosePoints sur le script Movement
        }
        else if (collision.gameObject.CompareTag("AttackArea")) // Assurez-vous que la zone d'attaque a le tag "AttackArea"
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // Détruit l'ennemi
    }

    void SpawnRandomly()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
    }
}
