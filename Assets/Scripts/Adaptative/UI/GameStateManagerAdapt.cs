﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManagerAdapt : MonoBehaviour
{
    public GameState gameState;
    public EnemyManager enemyManager;
    public DungeonData dungeon;
    public ItemManager itemManager;

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
        SceneManager.LoadScene("BSPAdapt");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuAdapt");
        Time.timeScale = 1f;

    }

    public void Win()
    {
        //gameData.AddEnemyManager(enemyManager);
        //gameData.AddDungeon(dungeon);
        //gameData.AddItemManager(itemManager);
        Game game = new Game(itemManager.initialItems, itemManager.pickedItems, enemyManager.initialEnemies.Count, enemyManager.killedEnemies.Count);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);
        winCanvas.SetActive(true);
    }

    public void Loose()
    {
        //gameData.AddEnemyManager(enemyManager);
        //gameData.AddDungeon(dungeon);
        //gameData.AddItemManager(itemManager);
        Game game = new Game(itemManager.initialItems, itemManager.pickedItems, enemyManager.initialEnemies.Count, enemyManager.killedEnemies.Count);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);
        looseCanvas.SetActive(true);
    }
}
