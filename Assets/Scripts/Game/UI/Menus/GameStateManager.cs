using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameState gameState;
    public EnemyManager enemyManager;
    public GameData gameData;

    public GameObject winCanvas;
    public GameObject looseCanvas;

    public void Start()
    {
        Time.timeScale = 0f;
        gameState.win = false;
        gameState.loose = false;
    }

    public void Play()
    {
        SceneManager.LoadScene("BSP");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;

    }

    public void Win()
    {
        gameData.AddEnemyManager(enemyManager);
        winCanvas.SetActive(true);
    }

    public void Loose()
    {
        gameData.AddEnemyManager(enemyManager);
        looseCanvas.SetActive(true);
    }
}
