using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    public EnemyManager data;
    private List<Rect> rooms;

    // LOW, MEDIUM, HIGH (Room's evil level)
    private enum Evil { LOW, MEDIUM, HIGH };
    private List<Evil> evilLevel;

    // BAT (0), SKELETON (1), BLACK (2)
    public List<List<int>> enemiesInRooms;
    public int numOfEnemies;

    public EnemySpawner(List<Rect> rooms, EnemyManager data)
    {
        this.rooms = rooms;
        this.data = data;
        enemiesInRooms = new List<List<int>>();
    }

    // Associate a level of evil to each room, with the initial ones being less evil than the last ones
    private void EvilLevel()
    {
        evilLevel = new List<Evil>(rooms.Count);

        int low = (int)Mathf.Round(rooms.Count * data.lowPerc);
        int medium = (int)Mathf.Round(rooms.Count * data.mediumPerc);
        int high = (int)Mathf.Round(rooms.Count * data.highPerc);

        for (int i = 0; i < low; i++) { evilLevel.Add(Evil.LOW); }

        for (int i = 0; i < medium; i++) { evilLevel.Add(Evil.MEDIUM); }

        for (int i = 0; i < high; i++) { evilLevel.Add(Evil.HIGH); }
    }



    public List<List<int>> EnemiesInRooms()
    {
        EvilLevel();

        for (int i = 0; i < rooms.Count; i++)
        {
            enemiesInRooms.Add(new List<int>());

            if (i == 0)
            {
                if (rooms[0].height < data.minSize && rooms[0].width < data.minSize) { continue; }

                else { enemiesInRooms[0].Add(0); numOfEnemies++; }
            }

            else
            {
                switch (evilLevel[i])
                {
                    case Evil.LOW:
                        if (rooms[i].height < data.minSize && rooms[i].height < data.minSize)
                        {
                            int num = Random.Range(0, 1);
                            if (num == 1)
                            {
                                int enemy = Random.Range(0, 1);

                                if (enemy == 0) { enemiesInRooms[i].Add(0); numOfEnemies++; }
                                else { enemiesInRooms[i].Add(1); numOfEnemies++; }
                            }
                        }

                        else
                        {
                            int num = Random.Range(1, 3);
                            for (int e = 0; e < num; e++)
                            {
                                int enemy = Random.Range(0, 1);
                                if (enemy == 0) { enemiesInRooms[i].Add(0); numOfEnemies++; }
                                else { enemiesInRooms[i].Add(1); numOfEnemies++; }
                            }
                        }

                        break;

                    case Evil.MEDIUM:
                        if (rooms[i].height < data.minSize && rooms[i].height < data.minSize)
                        {
                            enemiesInRooms[i].Add(1);
                            numOfEnemies++;
                        }

                        else
                        {
                            int num = Random.Range(2, 4);
                            for (int e = 0; e < num; e++)
                            {
                                int enemy = Random.Range(0, 1);
                                if (enemy == 0) { enemiesInRooms[i].Add(0); numOfEnemies++; }
                                else { enemiesInRooms[i].Add(1); numOfEnemies++; }
                            }
                        }

                        break;

                    case Evil.HIGH:

                        if (rooms[i].height < data.minSize && rooms[i].height < data.minSize)
                        {
                            enemiesInRooms[i].Add(2);
                            numOfEnemies++;
                        }

                        else
                        {
                            int num = Random.Range(3, 5);
                            for (int e = 0; e < num; e++)
                            {
                                int enemy = Random.Range(0, 1);
                                if (enemy == 0) { enemiesInRooms[i].Add(1); numOfEnemies++; }
                                else { enemiesInRooms[i].Add(2); numOfEnemies++; }
                            }
                        }

                        break;
                }
            }
        }
        data.numOfEnemies = numOfEnemies;
        //data.enemiesInRoom = enemiesInRooms;

        return enemiesInRooms;
    }

    /*
     public void SpawnEnemies(List<Rect> rooms)
     {
         EnemiesInRooms(rooms);
         spawnedEnemies = new GameObject[numOfEnemies];
         int numEnem = 0;
         for (int i = 0; i < rooms.Count; i++)
         {
             for ( int j = 0; j < enemiesInRooms[i].Count; j++)
             {
                 Rect temp = rooms[i];
                 float x = Random.Range(temp.x + 1, temp.x + temp.width - 1);
                 float y = Random.Range(temp.y + 1, temp.y + temp.height - 1);
                 switch (enemiesInRooms[i][j])
                 {
                     case Enemies.BAT:
                         spawnedEnemies[numEnem] = Instantiate(enemyType[0], new Vector3(x, y, 0f), Quaternion.identity);
                         numEnem++;
                         break;
                     case Enemies.SKELETON:
                         spawnedEnemies[numEnem] = Instantiate(enemyType[1], new Vector3(x, y, 0f), Quaternion.identity);
                         numEnem++;
                         break;
                     case Enemies.BLACK:
                         spawnedEnemies[numEnem] = Instantiate(enemyType[2], new Vector3(x, y, 0f), Quaternion.identity);
                         numEnem++;
                         break;
                 }
             }   
         }

     }*/
}
