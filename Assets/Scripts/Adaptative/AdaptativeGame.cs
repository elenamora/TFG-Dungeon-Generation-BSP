using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeGame : MonoBehaviour
{
    public GameData gameData;
    public PlayerType[] playerTypes;

    private enum Type {STANDARD, EXPLORER, ACHIEVER, KILLER};

    private Type player;
    private int numGames;


    // Start is called before the first frame update
    void Start()
    {
        DataBaseHandler.GetGames(AuthHandler.userId, AuthHandler.idToken, games =>
        {
            numGames = games.Count;
        });
        //gameSave = GameSaveManager.gameSave;
        StartCoroutine(CoAssignPlayerType());

    }

    IEnumerator CoAssignPlayerType()
    {
        yield return new WaitForSeconds(0.1f);
        AssignPlayerType();
    }

    public void OnEnable()
    {
        DataBaseHandler.GetGames(AuthHandler.userId, AuthHandler.idToken, games =>
        {
            numGames = games.Count;
        });
        StartCoroutine(CoAssignPlayerType());
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
        if(numGames < 2) { player = Type.STANDARD; }

        else
        {
            float e = ComputeEnemiesLeft();
            float i = ComputeItemsPicked();

            if (e > 0.7 && i > 0.5) player = Type.EXPLORER;

            else if (e < 0.25 && i > 0.8) player = Type.ACHIEVER;

            else if (e < 0.25 && i < 0.5) player = Type.KILLER;

            else player = Type.STANDARD;
        }


    }

    // porcentaje de enemigos matados respecto a los enemigos que teníamos inicialmente. Luego se hace la media.
    public float ComputeEnemiesLeft()
    {
        float enemiesLeft = 0f;
        DataBaseHandler.GetGames(AuthHandler.userId, AuthHandler.idToken, games =>
        {
            foreach (var game in games)
            {
                int dif = game.Value.initialEnemies - game.Value.killedEnemies;
                enemiesLeft += dif / game.Value.initialEnemies;
            }
        });

        return enemiesLeft / numGames;

        /*
        for (int i = 0; i < gameData.enemies.Count; i++)
        {
            int dif = gameData.enemies[i].initialEnemies.Count - gameData.enemies[i].killedEnemies.Count;
            enemiesLeft += dif / gameData.enemies[i].initialEnemies.Count;

            Debug.Log("Initial enemies  " + gameData.enemies[i].initialEnemies.Count);
            Debug.Log("Killed Enenmies  " + gameData.enemies[i].killedEnemies.Count);
            Debug.Log("Dif = initial enemies - killed enemies  " + dif);

        }*/
    }

    public float ComputeItemsPicked()
    {
        float itemsPicked = 0f;
        DataBaseHandler.GetGames(AuthHandler.userId, AuthHandler.idToken, games =>
        {
            foreach (var game in games)
            {
                int dif = game.Value.initialItems - game.Value.killedEnemies;

                itemsPicked += dif / game.Value.initialItems;
            }
            
        });

        return itemsPicked / numGames;

        /*
        for( int i = 0; i < gameData.items.Count; i++)
        {
            int dif = gameData.items[i].initialItems - gameData.enemies[i].killedEnemies.Count;

            itemsPicked += dif / gameData.items[i].initialItems;


            Debug.Log("Initial items  " + gameData.items[i].initialItems);
            Debug.Log("Killed Enenmies  " + gameData.enemies[i].killedEnemies.Count);
            Debug.Log("Dif = initial items - killed enemies  " + dif);

        }*/
    }

}
