using System;

[Serializable]
public class User
{
    public string username;
    public int level;

    public User(string username, int level)
    {
        this.username = username;
        this.level = level;
    }

}
