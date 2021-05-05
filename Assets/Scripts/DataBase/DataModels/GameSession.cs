using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSession
{
    /*
    private string idToken;
    private string userId;
    private string refreshToken;
    private DateTime expiration;

    private bool refresingToken;

    public string IdToken { get => idToken; set => idToken = value; }
    public string UserId { get => userId; set => userId = value; }
    public string RefreshToken { get => refreshToken; set => refreshToken = value; }
    public DateTime Expiration { get => expiration; set => expiration = value; }
    public bool RefreshingToken { get => refresingToken; set => refresingToken = value; }
    
    public string idToken;
    public string userId;
    public string username;
    public int level;*/

    public Dictionary<string, Dictionary<string, Game>> games;

}
