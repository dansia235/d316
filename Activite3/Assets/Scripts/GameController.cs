using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int enemyCount = 0;

    public Chronometer chronometer;
    public UIManager uiManager;

    private bool isGamePaused = false;
    public bool isGameActive = false;

    public GameObject enemyPrefab; // Référence au prefab de l'ennemi
    public Transform enemySpawnPoint; // Point de spawn des ennemis

    private const float spawnInterval = 5.0f; // Intervalle de temps pour la génération d'ennemis


    private const int MaxEnemies = 50; // Nombre maximum d'ennemis


    void Start()
    {
        isGameActive = false;
        // Initialisation des autres composants du jeu si nécessaire
    }

    void Update()
    {
        // Gère la pause et la reprise du jeu avec la touche espace
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        isGamePaused = false;
        uiManager.StartGame();

        // Démarrer le chronomètre et la routine de génération d'ennemis
        if (chronometer != null)
        {
            chronometer.StartChronometer();
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (isGameActive && enemyCount < MaxEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);

            Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            IncrementEnemyCount();
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0; // Stoppe le temps dans le jeu
        uiManager.ShowPausePanel(); // Affiche le panneau de pause
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1; // Remet le temps en marche
        uiManager.HidePausePanel(); // Cache le panneau de pause
    }

    public void EndGame()
    {
        isGameActive = false;
        isGamePaused = false;

        // Informer l'UIManager de la fin du jeu
        if (uiManager != null)
        {
            uiManager.HandleEndGame();
        }

        // Arrêter le chronomètre
        if (chronometer != null)
        {
            chronometer.StopChronometer();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Méthode pour incrémenter le nombre d'ennemis et mettre à jour l'UI
    public void IncrementEnemyCount()
    {
        enemyCount++;
        UpdateEnemyCount();
    }

    // Méthode pour décrémenter le nombre d'ennemis et mettre à jour l'UI
    public void DecrementEnemyCount()
    {
        if (enemyCount > 0) enemyCount--;
        UpdateEnemyCount();
    }

    // Méthode pour mettre à jour l'UI avec le nombre d'ennemis actuel
    public void UpdateEnemyCount()
    {
        if (uiManager != null)
        {
            uiManager.UpdateEnemyCount(enemyCount);
        }
    }
}
