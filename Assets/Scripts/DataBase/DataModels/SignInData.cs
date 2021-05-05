using System;

[Serializable]
public class SignInData
{
    public string localId;
    public string email;
    public string displayName;
    public string idToken;

    public bool registered;

    public string refreshToken;
    public string expiresIn;
}
