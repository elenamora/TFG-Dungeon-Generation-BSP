using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignInUp : MonoBehaviour
{
    public InputField email;
    public InputField pswd;
    public Text hint;
    public InputField username;

    public Inventory playerInventory;

    public void SignIn()
    {
        AuthHandler.SignIn(email.text, pswd.text);
        email.text = "";
        pswd.text = "";
        StartCoroutine(CoSignIn());   
    }

    IEnumerator CoSignIn()
    {
        yield return new WaitForSeconds(1f);
        if (AuthHandler.resp == "OK")
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            hint.color = Color.red;
            hint.text = "Incorrect User or Password";
        }
    }

    public void SignUpMenu()
    {
        hint.color = Color.black;
        hint.text = "";
        email.text = "";
        pswd.text = "";
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
            AuthHandler.SignUp(email.text, pswd.text, new User(username.text, 1));
            email.text = "";
            pswd.text = "";
            username.text = "";
            StartCoroutine(CoSignUp());
        } 
    }

    IEnumerator CoSignUp()
    {
        yield return new WaitForSeconds(0.6f);
        if (AuthHandler.resp == "OK")
        {
            DataBaseHandler.PostInventory(new InventoryData(0, 0, 0, 0, 0), AuthHandler.userId, () => { }, AuthHandler.idToken);
            SceneManager.LoadScene("Menu");
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
