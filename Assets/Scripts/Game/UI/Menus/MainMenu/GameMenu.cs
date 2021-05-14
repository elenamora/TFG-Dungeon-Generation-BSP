using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject gameMenu;
    public GameObject inventoryMenu;
    public GameObject weaponSelectionMenu;

    public Text username;
    public Text level;

    public GameData gameData;

    public void Start()
    {
        DataBaseHandler.GetUser(AuthHandler.userId, (user) => {
            gameData.username = user.username;
            gameData.level = user.level;
            username.text = gameData.username;
            level.text = gameData.level.ToString();

        }, AuthHandler.idToken);
    }

    public void GoToInventory()
    {
        DataBaseHandler.GetInventory(AuthHandler.userId, (inventory) => {
            foreach (InventoryItem item in playerInventory.inventoryItems)
            {
                switch (item.name)
                {
                    case "Coin":
                        item.quantity = inventory.coin;
                        break;
                    case "Health Potion":
                        item.quantity = inventory.healthPotion;
                        break;
                    case "Energy Potion":
                        item.quantity = inventory.energyPotion;
                        break;
                    case "Gem":
                        item.quantity = inventory.gem;
                        break;
                    case "Key":
                        item.quantity = inventory.key;
                        break;
                }
            }

            StartCoroutine(CoRout());

        }, AuthHandler.idToken);
    }

    IEnumerator CoRout()
    {
        yield return new WaitForSeconds(1f);
        gameMenu.SetActive(false);
        inventoryMenu.SetActive(true);

    }

    public void GoToGame()
    {
        DataBaseHandler.GetInventory(AuthHandler.userId, (inventory) => {

            foreach(InventoryItem item in playerInventory.inventoryItems)
            {
                switch (item.name)
                {
                    case "Coin":
                        item.quantity = inventory.coin;
                        break;
                    case "Health Potion":
                        item.quantity = inventory.healthPotion;
                        break;
                    case "Energy Potion":
                        item.quantity = inventory.energyPotion;
                        break;
                    case "Gem":
                        item.quantity = inventory.gem;
                        break;
                    case "Key":
                        item.quantity = inventory.key;
                        break;
                }
            }

            StartCoroutine(CoGoToGame());

        }, AuthHandler.idToken);
    }

    IEnumerator CoGoToGame()
    {
        yield return new WaitForSeconds(1f);
        gameMenu.SetActive(false);
        weaponSelectionMenu.SetActive(true);

    }

    public void ExitGame()
    {
        SceneManager.LoadScene("LoginRegister");
    }

}
