using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject homeScreenPanel;
    public GameObject gameplayUIPanel;
    public GameObject gameOverPanel;
    public GameObject gamePausePanel;

    public Text gemstoneCountText;
    public Text playerLivesText;
    public PlayerController player;

    public Text enemyCountText;

    public GameController gameController; // Ajoutez cette référence

    public void UpdateEnemyCount(int count)
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Ennemis: " + count.ToString();
        }
    }

    public void OnReplayButtonClicked()
    {
        // Réinitialisez le jeu
        gameController.StartGame();
        // Cachez le panneau de fin de jeu et montrez le panneau de jeu
        gameOverPanel.SetActive(false);
        gameplayUIPanel.SetActive(true);
    }

    public void OnContinueButtonClicked()
    {
        // Reprendre le jeu
        if (gameController != null)
        {
            gameController.ResumeGame();
        }
        // Cachez le panneau de pause
        gamePausePanel.SetActive(false);
    }


    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        Debug.Log("Quitter le jeu (ne fonctionne qu'en mode éditeur)");
        
#else
        Application.Quit();
#endif
    }

    public void OnPlayButtonClicked()
    {
        // Ici, appelez la méthode StartGame de GameController
        if (gameController != null)
        {
            gameController.StartGame();
        }
        // Vous pouvez également gérer d'autres éléments de l'UI ici, comme cacher le HomeScreenPanel
        homeScreenPanel.SetActive(false);
    }

    public void UpdateGemstoneCount(int count)
    {
        if (gemstoneCountText != null)
        {
            gemstoneCountText.text = "Pierres: " + count.ToString();
        }
    }

    public void UpdateLivesCount(int lives)
    {
        if (playerLivesText != null)
        {
            playerLivesText.text = "Vies: " + lives.ToString();
        }
    }

    void Start()
    {
        gemstoneCountText = GameObject.Find("QuantitePC").GetComponent<Text>();
        playerLivesText = GameObject.Find("QuantiteVR").GetComponent<Text>();

        ShowHomeScreen();
    }

    void Update()
    {
        UpdateGemstoneUI();
    }

    void UpdateGemstoneUI()
    {
        if (player != null && gemstoneCountText != null)
        {
            gemstoneCountText.text = "Pierres: " + player.GetGemstoneCount().ToString();
        }

        if (player != null && playerLivesText != null)
        {
            playerLivesText.text = "Vies: " + player.GetLives().ToString();
        }
    }

    public void ShowHomeScreen()
    {
        homeScreenPanel.SetActive(true);
        gameplayUIPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePausePanel.SetActive(false);
    }

    public void StartGame()
    {
        homeScreenPanel.SetActive(false);
        gameplayUIPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void HandleEndGame()
    {
        // Cette méthode est appelée par GameController pour gérer la fin du jeu
        homeScreenPanel.SetActive(false);
        gameplayUIPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        gamePausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        gamePausePanel.SetActive(false);
    }
}