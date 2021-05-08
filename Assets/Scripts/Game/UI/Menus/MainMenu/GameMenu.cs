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
            playerInventory.inventoryItems[0].quantity = inventory.coin;
            playerInventory.inventoryItems[1].quantity = inventory.healthPotion;
            playerInventory.inventoryItems[2].quantity = inventory.energyPotion;
            playerInventory.inventoryItems[3].quantity = inventory.key;
            playerInventory.inventoryItems[4].quantity = inventory.gem;

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
            playerInventory.inventoryItems[0].quantity = inventory.coin;
            playerInventory.inventoryItems[1].quantity = inventory.healthPotion;
            playerInventory.inventoryItems[2].quantity = inventory.energyPotion;
            playerInventory.inventoryItems[3].quantity = inventory.key;
            playerInventory.inventoryItems[4].quantity = inventory.gem;

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
