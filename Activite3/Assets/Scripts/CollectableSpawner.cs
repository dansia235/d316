using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject[] stonePrefabs; // Vos préfabs de pierres
    public float spawnInterval = 2.0f; // Intervalle entre les spawns
    public float stoneLifetime = 5.0f; // Durée de vie des pierres

    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = CalculateScreenBounds();
        InvokeRepeating("SpawnStone", 0.0f, spawnInterval);
    }

    Vector2 CalculateScreenBounds()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        return new Vector2(cameraWidth / 2, cameraHeight / 2);
    }

    void SpawnStone()
    {
        if (stonePrefabs.Length == 0)
        {
            Debug.LogError("Aucun préfab de stone n'est assigné au script CollectableSpawner.");
            return;
        }

        int randomIndex = Random.Range(0, stonePrefabs.Length);

        if (stonePrefabs[randomIndex] != null)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-screenBounds.x, screenBounds.x),
                Random.Range(-screenBounds.y, screenBounds.y)
            ) + (Vector2)mainCamera.transform.position;

            GameObject stone = Instantiate(stonePrefabs[randomIndex], spawnPosition, Quaternion.identity);
            Destroy(stone, stoneLifetime); // Détruit le pierre après 'stoneLifetime' secondes
        }
        else
        {
            Debug.LogError("Le préfab de stone à l'indice " + randomIndex + " est nul.");
        }
    }
}
