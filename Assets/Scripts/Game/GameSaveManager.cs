using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;

    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public GameData gameData;

    private void Awake()
    {
        // We only want one gameSave object
        if ( gameSave == null) { gameSave = this; }

        else { Destroy(this.gameObject); }

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
        SaveGames();
        SaveEnemies();
    }

    public void SaveScriptables()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();

            Debug.Log(Application.persistentDataPath);

            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);

            file.Close();
        }
    }

    // We save the information of every Dungeon created (every game)
    public void SaveGames()
    {
        for (int i = 0; i < gameData.dungeons.Count; i++) { 
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/Game{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();

            var json = JsonUtility.ToJson(gameData.dungeons[i]);
            binary.Serialize(file, json);

            file.Close();
        }
    }

    // We save the information about the enemies of every game created (same as dungeons)
    public void SaveEnemies()
    {
        for (int i = 0; i < gameData.enemies.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/Enemy{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();

            var json = JsonUtility.ToJson(gameData.enemies[i]);
            binary.Serialize(file, json);

            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);

                file.Close();
            }
        }
    }

    public void LoadGames()
    {
        for (int i = 0; i < gameData.dungeons.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/Game{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/Game{0}.dat", i), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), gameData.dungeons[i]);

                file.Close();
            }
        }
    }

    public void LoadEnemies()
    {
        for (int i = 0; i < gameData.enemies.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/Enemy{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/Enemy{0}.dat", i), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), gameData.enemies[i]);

                file.Close();
            }
        }
    }




    public void ResetScriptables()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }
}
