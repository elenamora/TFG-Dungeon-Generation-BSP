using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManagerAdapt : MonoBehaviour
{
    public GameState gameState;
    public EnemyManager enemyManager;
    public DungeonData dungeon;
    public ItemManager itemManager;

    public GameData gameData;

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
        Time.timeScale = 1f;
        StartCoroutine(CoPlay());
    }

    IEnumerator CoPlay()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("BSPAdapt");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuAdapt");
        Time.timeScale = 1f;

    }

    public void Win()
    {
        Game game = new Game(itemManager.initialItems, itemManager.pickedItems, enemyManager.initialEnemies.Count, enemyManager.killedEnemies.Count);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);

        UpdateInventory();
        gameData.level++;
        DataBaseHandler.PostUser(new User(gameData.username, gameData.level), AuthHandler.userId, () => { }, AuthHandler.idToken);

        winCanvas.SetActive(true);
        //StartCoroutine(CoUpdateUserLevel());
    }

    IEnumerator CoUpdateUserLevel()
    {
        yield return new WaitForSeconds(0.1f);
        DataBaseHandler.PostUser(new User(gameData.username, gameData.level), AuthHandler.userId, () => { }, AuthHandler.idToken);

    }

    public void Loose()
    {
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
