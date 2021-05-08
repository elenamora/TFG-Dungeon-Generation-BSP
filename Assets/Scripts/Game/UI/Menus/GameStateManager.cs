using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameState gameState;

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
        SceneManager.LoadScene("BSP");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;

    }

    public void Win()
    {
        UpdateInventory();
        winCanvas.SetActive(true);
        //gameData.level++;
        //StartCoroutine(CoUpdateUserLevel());
    }

    IEnumerator CoUpdateUserLevel()
    {
        yield return new WaitForSeconds(0.1f);
        DataBaseHandler.PostUser(new User(gameData.username, gameData.level), AuthHandler.userId, () => { }, AuthHandler.idToken);

    }

    public void Loose()
    {
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
