using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemstoneSpawner : MonoBehaviour
{
    public GameObject[] gemstonePrefabs; // Tableau des prefabs de pierres
    public float spawnInterval = 2.0f; // Intervalle de temps pour la génération

    private float timeSinceLastSpawn;
    public GameController gameController; // Ajoutez une référence au GameController

    void Start()
    {
        timeSinceLastSpawn = 0;
    }

    void Update()
    {
        // Vérifiez si le jeu est actif avant de générer des pierres
        if (gameController.isGameActive)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnGemstone();
                timeSinceLastSpawn = 0;
            }
        }
    }

    void SpawnGemstone()
    {
        // Choisissez un prefab de pierre aléatoire
        GameObject prefabToSpawn = gemstonePrefabs[Random.Range(0, gemstonePrefabs.Length)];
        // Définissez une position aléatoire pour la pierre
        Vector2 spawnPosition = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)); // Ajustez ces valeurs en fonction de votre carte
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
