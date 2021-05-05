using System;
using System.Collections.Generic;

[Serializable]
public class Game
{
    public int initialItems;
    public int itemsPicked;
    public int initialEnemies;
    public int killedEnemies;

    public Game(int initialItems, int itemsPicked, int initialEnemies, int killedEnemies)
    {
        this.initialItems = initialItems;
        this.itemsPicked = itemsPicked;
        this.initialEnemies = initialEnemies;
        this.killedEnemies = killedEnemies;
    }
}
