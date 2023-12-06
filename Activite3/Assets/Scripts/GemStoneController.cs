using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemStoneController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.CollectGemstone();
            }
            Destroy(gameObject);
        }
    }
}
