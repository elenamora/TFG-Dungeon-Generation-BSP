using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Controls the state of the game and the possibilities that the player has
 * 
 */

public class GameStateManager : MonoBehaviour
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

    /*
     * Function called when starting a new game. It will reload the BSP scene in order to create everything from scratch
     */
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

    /*
     * Function called from the pause, win and loose menus that allows the player to go back to the scene Menu
     */
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;

    }

    /*
     * Function called when the event Win is raised.
     *  Post all the game related information to the database.
     *  Update the inventory with the picked up and spent object from that particular game.
     *  Update the user level.
     *  Activate the Win canvas.
     */
    public void Win()
    {
        Game game = new Game(itemManager.initialItems, itemManager.pickedItems, enemyManager.initialEnemies.Count, enemyManager.killedEnemies.Count);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);

        UpdateInventory();
        gameData.level++;
        DataBaseHandler.PostUser(new User(gameData.username, gameData.level), AuthHandler.userId, () => { }, AuthHandler.idToken);

        winCanvas.SetActive(true);
    }

    /*
     * Function called when the event Loose is raised.
     *  Post all the game related information to the database.
     *  Update the inventory with the picked up and spent object from that particular game.
     *  Activate the Loose canvas.
     */
    public void Loose()
    {
        Game game = new Game(itemManager.initialItems, itemManager.pickedItems, enemyManager.initialEnemies.Count, enemyManager.killedEnemies.Count);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);

        UpdateInventory();
        looseCanvas.SetActive(true);
    }

    /*
     * Function called when the game finishes, whether the player wins or looses.
     *  Post the actualized number of inventory items to the player inventory in the database
     */
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
