using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePrefab : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Assurez-vous que votre personnage principal a le tag "Player"
        {
            Destroy(gameObject); // Détruit le fruit
        }
    }
}
