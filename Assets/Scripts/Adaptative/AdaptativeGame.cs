using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeGame : MonoBehaviour
{
    public GameData gameData;
    public PlayerType[] playerTypes;

    private enum Type {STANDARD, EXPLORER, ACHIEVER, KILLER};

    private GameSaveManager gameSave;
    private Type player;

    // Start is called before the first frame update
    void Start()
    {
        gameSave = GameSaveManager.gameSave;
        AssignPlayerType();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnEnable()
    {
        //gameData.playerType = playerTypes[0];
        AssignPlayerType();
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
        if(gameData.enemies.Count < 2) { player = Type.STANDARD; Debug.Log("hola"); }

        else
        {
            float e = ComputeEnemiesLeft();
            Debug.Log(e);

            if (e > 0.7) player = Type.EXPLORER;

            else if (e < 0.3) player = Type.KILLER;

            else player = Type.STANDARD;
        }


    }

    // porcentaje de enemigos matados respecto a los enemigos que teníamos inicialmente. Luego se hace la media.
    public float ComputeEnemiesLeft()
    {
        float enemiesLeft = 0;
        foreach(EnemyManager e in gameData.enemies)
        {
            int dif = e.initialEnemies.Count - e.killedEnemies.Count;
            enemiesLeft += dif / e.initialEnemies.Count;
        }

        Debug.Log(enemiesLeft / gameData.enemies.Count);

        return enemiesLeft / gameData.enemies.Count;
    }


}
