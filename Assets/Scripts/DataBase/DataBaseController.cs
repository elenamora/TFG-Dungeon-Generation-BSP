﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseController : MonoBehaviour
{
    /*
    public void SIn()
    {
        AuthHandler.SignIn(email.text, pswd.text);
    }

    public void SUp()
    {
        AuthHandler.SignUp(email.text, pswd.text, new User("Inés"));
    }*/

    public void PlayGame()
    {
        Game game = new Game(1, 1, 1, 1);
        DataBaseHandler.PostGame(game, AuthHandler.userId, () => { }, AuthHandler.idToken);
    }

    public void GetGames()
    {
        DataBaseHandler.GetGames(AuthHandler.userId, AuthHandler.idToken, games =>
        {
            foreach (var game in games)
            {
                Debug.Log($"{game.Value.initialItems} {game.Value.initialEnemies}");
            }
        });
    }

    public void PostInventory()
    {
        InventoryData inventory = new InventoryData(10, 1, 2, 3, 4);
        DataBaseHandler.PostInventory(inventory, AuthHandler.userId, () => { }, AuthHandler.idToken);
    }

    public void GetInventory()
    {
        DataBaseHandler.GetInventory(AuthHandler.userId, (inventory) => {
            Debug.Log(inventory.coins);
            Debug.Log(inventory.gems);

        }, AuthHandler.idToken);
    }
}