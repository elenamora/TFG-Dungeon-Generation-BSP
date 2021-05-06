using System.Collections;
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
        AuthHandler.SignIn(email.text, pswd.text);
        email.text = "";
        pswd.text = "";
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
            email.text = "";
            pswd.text = "";
            username.text = "";
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
        yield return new WaitForSeconds(0.6f);
        if (AuthHandler.resp == "OK")
        {
            DataBaseHandler.PostInventory(new InventoryData(1, 3, 2, 1, 1), AuthHandler.userId, () => { }, AuthHandler.idToken);
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
