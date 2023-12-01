using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Vos préfabs de fruits
    public float spawnInterval = 2.0f; // Intervalle entre les spawns
    public float fruitLifetime = 5.0f; // Durée de vie des fruits

    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = CalculateScreenBounds();
        InvokeRepeating("SpawnFruit", 0.0f, spawnInterval);
    }

    Vector2 CalculateScreenBounds()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        return new Vector2(cameraWidth / 2, cameraHeight / 2);
    }

    void SpawnFruit()
    {
        if (fruitPrefabs.Length == 0)
        {
            Debug.LogError("Aucun préfab de fruit n'est assigné au script CollectableSpawner.");
            return;
        }

        int randomIndex = Random.Range(0, fruitPrefabs.Length);

        if (fruitPrefabs[randomIndex] != null)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-screenBounds.x, screenBounds.x),
                Random.Range(-screenBounds.y, screenBounds.y)
            ) + (Vector2)mainCamera.transform.position;

            GameObject fruit = Instantiate(fruitPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            Destroy(fruit, fruitLifetime); // Détruit le fruit après 'fruitLifetime' secondes
        }
        else
        {
            Debug.LogError("Le préfab de fruit à l'indice " + randomIndex + " est nul.");
        }
    }
}
