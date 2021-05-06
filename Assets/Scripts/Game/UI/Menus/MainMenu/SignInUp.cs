﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInUp : MonoBehaviour
{
    public Text email;
    public Text pswd;
    public Text hint;
    public Text username;

    [Header("Menus")]
    public GameObject gameMenu;
    public GameObject mainMenu;

    public void SignIn()
    {
        AuthHandler.SignIn(email.text, pswd.text);
        StartCoroutine(CoSign());
    }

    public void SignUp()
    {
        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(pswd.text) || string.IsNullOrEmpty(username.text))
        {
            hint.color = Color.red;
            hint.text = "All fields must be filled";
        }
        else
        {
            AuthHandler.SignUp(email.text, pswd.text, new User(username.text));
            StartCoroutine(CoSign());
        } 
    }

    public void SignUpMenu()
    {
        hint.color = Color.black;
        hint.text = "";
    }

    IEnumerator CoSign()
    {
        yield return new WaitForSeconds(0.5f);
        if (AuthHandler.resp == "OK")
        {
            gameMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else
        {
            hint.color = Color.red;
            hint.text = "Incorrect User or Password";
        }
    }

}