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

    public Inventory inventory;

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

        UpdateInventory();

        winCanvas.SetActive(true);
    }

    public void Loose()
    {
        //gameData.AddEnemyManager(enemyManager);
        //gameData.AddDungeon(dungeon);
        //gameData.AddItemManager(itemManager);
        Game game = new Game(itemManager.initialItems, itemManager.pickedItems, enemyManager.initialEnemies.Count, enemyManager.killedEnemies.Count);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);

        UpdateInventory();

        looseCanvas.SetActive(true);
    }


    public void UpdateInventory()
    {
        int coin = 0, health = 0, energy = 0, key = 0, gem = 0;

        foreach (InventoryItem item in inventory.inventoryItems)
        {
            switch (item.itemName)
            {
                case "Coin":
                    coin = item.quantity;
                    break;
                case "Health Potion":
                    health = item.quantity;
                    break;
                case "Energy Potion":
                    energy = item.quantity;
                    break;
                case "Gem":
                    gem = item.quantity;
                    break;
                case "Key":
                    key = item.quantity;
                    break;
            }
        }

        InventoryData temp = new InventoryData(coin, health, energy, key, gem);
        DataBaseHandler.PostInventory(temp, AuthHandler.userId, () => { }, AuthHandler.idToken);
    }
}
