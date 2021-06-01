using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private List<Rect> rooms;

    // LOW, MEDIUM, HIGH (Room's evil level)
    private enum Evil { LOW, MEDIUM, HIGH };
    private List<Evil> evilLevel;

    // BAT (0), SKELETON (1), BLACK (2)
    public List<List<int>> enemiesInRooms;
    public int numOfEnemies;
    private int minSize;

    public EnemySpawner(List<Rect> rooms)
    {
        this.rooms = rooms;
        minSize = 10;
        enemiesInRooms = new List<List<int>>();
    }

    // Associate a level of evil to each room, with the initial ones being less evil than the last ones
    private void EvilLevel()
    {
        evilLevel = new List<Evil>(rooms.Count);

        int low = (int)Mathf.Round(rooms.Count * 0.2f);
        int medium = (int)Mathf.Round(rooms.Count * 0.5f);
        int high = (int)Mathf.Round(rooms.Count * 0.3f);

        for (int i = 0; i < low; i++) { evilLevel.Add(Evil.LOW); }

        for (int i = 0; i < medium; i++) { evilLevel.Add(Evil.MEDIUM); }

        for (int i = 0; i < high; i++) { evilLevel.Add(Evil.HIGH); }
    }


    /*
     * Once we have the Evil level of each room a number of enemies will be added taking into account the aforementioned level.
     */
    public List<List<int>> EnemiesInRooms()
    {
        EvilLevel();

        //An bat will be added in the same room where the player starts only if its big enough.
        for (int i = 0; i < rooms.Count; i++)
        {
            enemiesInRooms.Add(new List<int>());

            if (i == 0)
            {
                if (rooms[0].height < minSize && rooms[0].width < minSize) { continue; }

                else { enemiesInRooms[0].Add(0); numOfEnemies++; }
            }

            else
            {
                switch (evilLevel[i])
                {
                    // In the LOW evil rooms there will be bats or skeletons
                    case Evil.LOW:
                        // If the room is too small we'll only have 0 or 1 enemy
                        if (rooms[i].height < minSize && rooms[i].height < minSize)
                        {
                            int num = Random.Range(0, 1);
                            if (num == 1)
                            {
                                int enemy = Random.Range(0, 1);

                                if (enemy == 0) { enemiesInRooms[i].Add(0); numOfEnemies++; }
                                else { enemiesInRooms[i].Add(1); numOfEnemies++; }
                            }
                        }

                        // If the room is big enough we'll have between 1 and 3 enemies.
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

                    // In the MEDIUM evil rooms there will be bats or skeletons
                    case Evil.MEDIUM:
                        // If the room is too small we'll have an skeleton
                        if (rooms[i].height < minSize && rooms[i].height < minSize)
                        {
                            enemiesInRooms[i].Add(1);
                            numOfEnemies++;
                        }
                        // If the room is big enough we'll have between 2 and 4 enemies.
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

                    // In the HIGH evil rooms there will be skeletons or black enemies
                    case Evil.HIGH:
                        // If the room is too small we'll have a black enemy
                        if (rooms[i].height < minSize && rooms[i].height < minSize)
                        {
                            enemiesInRooms[i].Add(2);
                            numOfEnemies++;
                        }
                        // If the room is big enough we'll have between 3 and 5 enemies.
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
        return enemiesInRooms;
    }

}
