using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;

public static class AuthHandler
{
    private const string apiKey = "AIzaSyCoREi7iwuL7KJoZzquSDpWv_0SgVRKl0Y";

    public delegate void SignUpSuccess();
    public delegate void SignInSuccess();

    public static GameSession gameSession = new GameSession();

    public static string idToken; // Key that proves the request is authenticated and the identity of the user
    public static string userId;

    public static string resp;

    /*
     * Sings up user with Firebase Auth using Email and Password method
     * Uploads the user object to Firebase Database
     *
     * @param email -- User's email
     * @param password -- User's password
     * @param user -- User object, which gets uploaded to Firebase Database
    */
    public static void SignUp(string email, string password, User user)
    {
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post<SignUpData>($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}",
            payLoad).Then(
            response =>
            {
                Debug.Log("User Created");
                resp = "OK";
                DataBaseHandler.PostUser(user, response.localId, () => { }, response.idToken);
                
            }).Catch(error =>
            {
                Debug.Log(error);
                resp = "NOK";
            });
    }

    /*
     * Sings in user with Firebase Auth using Email and Password method
     * Assigns the token and user id to global variables to be able to use Database functions
     *
     * @param email -- User's email
     * @param password -- User's password
     * @param user -- User object, which gets uploaded to Firebase Database
    */
    public static void SignIn(string email, string password)
    {
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post<SignInData>($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}",
            payLoad).Then(
            response =>
            {
                Debug.Log("Log in User");

                userId = response.localId;
                idToken = response.idToken;
                resp = "OK";
                DataBaseHandler.GetUser(userId, user => { }, response.idToken);
                
            }).Catch(error =>
                {
                    Debug.Log(error);
                    resp = "NOK";
                });
        //return "";
    }

}
