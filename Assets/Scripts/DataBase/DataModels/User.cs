using System;
using System.Collections.Generic;

[Serializable]
public class User
{
    public string username;
    public int level;

    public User(string username)
    {
        this.username = username;
        level = 1;
    }

}
