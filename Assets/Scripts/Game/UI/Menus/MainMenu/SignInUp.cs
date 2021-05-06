﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInUp : MonoBehaviour
{
    public InputField email;
    public InputField pswd;
    public Text hint;
    public InputField username;

    [Header("Menus")]
    public GameObject gameMenu;
    public GameObject mainMenu;

    public void SignIn()
    {
        email.text = "";
        pswd.text = "";
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
            email.text = "";
            pswd.text = "";
            username.text = "";
            AuthHandler.SignUp(email.text, pswd.text, new User(username.text));
            StartCoroutine(CoSign());
        } 
    }

    public void SignUpMenu()
    {
        hint.color = Color.black;
        hint.text = "";
        email.text = "";
        pswd.text = "";
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

    public void BackToSignIn()
    {
        email.text = "";
        pswd.text = "";
        username.text = "";
    }
}
