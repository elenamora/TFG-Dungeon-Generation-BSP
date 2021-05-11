using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeGame : MonoBehaviour
{
    public GameData gameData;
    public PlayerType[] playerTypes;

    private enum Type {STANDARD, EXPLORER, ACHIEVER, KILLER};

    private Type player;
    private float enemiesPerc;
    private float itemsPerc;
    private int numGames;


    // Start is called before the first frame update
    void Start()
    {
        ComputeEnemiesItems();
        Invoke("AssignPlayerType", 0.5f);

    }

    public void AssignPlayerType()
    {
        PickPlayer();
        switch (player)
        {
            case Type.STANDARD:
                gameData.playerType = playerTypes[0];
                break;
            case Type.EXPLORER:
                gameData.playerType = playerTypes[1];
                break;
            case Type.ACHIEVER:
                gameData.playerType = playerTypes[2];
                break;
            case Type.KILLER:
                gameData.playerType = playerTypes[3];
                break;
            default:
                break;
        }
    }

    public void PickPlayer()
    {
        if (numGames < 2) { player = Type.STANDARD; }

        else
        {
            if (enemiesPerc > 0.7 && itemsPerc > 0.5) player = Type.EXPLORER;

            else if (enemiesPerc < 0.25 && itemsPerc > 0.8) player = Type.ACHIEVER;

            else if (enemiesPerc < 0.25 && itemsPerc < 0.5) player = Type.KILLER;

            else player = Type.STANDARD;
        }


    }

    // porcentaje de enemigos matados respecto a los enemigos que teníamos inicialmente. Luego se hace la media.
    public void ComputeEnemiesItems()
    {
        float enemiesLeft = 0f;
        float itemsPicked = 0f;
        DataBaseHandler.GetGames(AuthHandler.userId, AuthHandler.idToken, games =>
        {
            foreach (var game in games)
            {
                // Enemies
                float en = game.Value.initialEnemies - game.Value.killedEnemies;
                enemiesLeft += en / game.Value.initialEnemies;

                // Items
                // it: number of items picked without taking into account the loot items from killed enemies
                float it;
                if (game.Value.killedEnemies < game.Value.itemsPicked)
                    it = game.Value.itemsPicked - game.Value.killedEnemies;
                else it = 0;
                 
                itemsPicked += it / game.Value.initialItems;
                Debug.Log(itemsPicked);
            }

            numGames = games.Count;

            enemiesPerc = enemiesLeft / numGames;
            itemsPerc = itemsPicked / numGames;
            
        });

    }


}
