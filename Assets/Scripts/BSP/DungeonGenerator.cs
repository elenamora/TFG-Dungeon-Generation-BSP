using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
	public int DUNGEON_W, DUNGEON_H;
	public int MIN_ROOM_SIZE;
	public GameObject floorTile;
	public GameObject hallwayTile;
	public GameObject wallTile;

	public GameObject player;
	public GameObject [] enemies;

	public GameObject bounds;

	private GameObject[,] roomFloor;

	public List<Rect> rooms;

	/*** SPAWN ENEMY VARIABLES ***/
	public Spawner spawner;
	public GameObject[] spawnedEnemies;
	public List<List<int>> enemiesInRooms;

	/*** ***/
	public GameObject[] items;

	void Start()
	{
		Leaf root = new Leaf(new Rect(0, 0, DUNGEON_W, DUNGEON_H));
		BSPTree tree = new BSPTree();
		tree.CreateTree(root, MIN_ROOM_SIZE);
		root.CreateRoom(MIN_ROOM_SIZE, rooms);

		spawner = new Spawner(rooms);

		roomFloor = new GameObject[DUNGEON_W, DUNGEON_H];
		DrawRooms(root);
		DrawHallways(root);
		DrawWalls(root);

		DrawPlayer();
		DrawBounds(root);

		DrawEnemies();

	}

	public void DrawWalls(Leaf root)
    {
		for (int i = (int) root.rect.x; i < root.rect.xMax; i++)
		{
			for (int j = (int) root.rect.y; j < root.rect.yMax; j++)
            {
				if (roomFloor[i, j] == null)
                {
					GameObject instance = Instantiate(wallTile, new Vector3(i, j, 0f), Quaternion.identity);
					instance.transform.SetParent(transform);
					roomFloor[i, j] = instance;
				}
				
				// We draw an extra column of wall tiles on the right of the dungeon
				GameObject inst2 = Instantiate(wallTile, new Vector3(root.rect.xMax, j, 0f), Quaternion.identity);
				inst2.transform.SetParent(transform);
				roomFloor[i, j] = inst2;

			}
        }
    }

	public void DrawBounds(Leaf root)
    {

		bounds = Instantiate(bounds, new Vector3(0, 0, 0f), Quaternion.identity);

		Vector3 temp = bounds.transform.localScale;

		temp.x = root.rect.xMax;
		temp.y = root.rect.yMax;

		bounds.transform.localScale = temp;

		BoxCollider2D collider = bounds.GetComponent<BoxCollider2D>();

		collider.size = new Vector2(root.rect.xMax, root.rect.yMax);
	}

	public void DrawPlayer()
    {
		//int index = Random.Range(0, rooms.Count);
		Rect temp = rooms[0];
		float x = Random.Range(temp.x + 1, temp.x + temp.width - 1);
		float y = Random.Range(temp.y + 1, temp.y + temp.height - 1);

		player = Instantiate(player, new Vector3(x, y, 0f), Quaternion.identity);

		int i = 0;
		/*
		foreach (GameObject item in items)
        {
			Instantiate(item, new Vector3(x, Random.Range(temp.y + 1 + i, temp.y + temp.height - 1 - i), 0f), Quaternion.identity);
			i++;
		}*/
		GameObject pot = Instantiate(items[3], new Vector3(x, Random.Range(temp.y + 1 + i, temp.y + temp.height - 1 - i), 0f), Quaternion.identity);
		GameObject redPotion = Instantiate(items[0], new Vector3(x, Random.Range(temp.y + 4, temp.y + temp.height - 4), 0f), Quaternion.identity);



		//float x2 = Random.Range(temp.x + 1, temp.x + temp.width - 1);
		//float y2 = Random.Range(temp.y + 1, temp.y + temp.height - 1);

	}

	public void DrawEnemies()
    {

        enemiesInRooms = spawner.EnemiesInRooms();

		spawnedEnemies = new GameObject[spawner.numOfEnemies];

		Debug.Log(spawner.numOfEnemies);

		int numEnem = 0;

		for (int i = 0; i < rooms.Count; i++)
		{
			for (int j = 0; j < enemiesInRooms[i].Count; j++)
			{
				Rect temp = rooms[i];
				float x = Random.Range(temp.x + 1, temp.x + temp.width - 1);
				float y = Random.Range(temp.y + 1, temp.y + temp.height - 1);

				switch (enemiesInRooms[i][j])
				{
					case 0:
						spawnedEnemies[numEnem] = Instantiate(enemies[0], new Vector3(x, y, 0f), Quaternion.identity);
						numEnem++;
						break;

					case 1:
						spawnedEnemies[numEnem] = Instantiate(enemies[1], new Vector3(x, y, 0f), Quaternion.identity);
						numEnem++;
						break;

					case 2:
						spawnedEnemies[numEnem] = Instantiate(enemies[2], new Vector3(x, y, 0f), Quaternion.identity);
						numEnem++;
						break;
				}
			}
		}

	}

	public void DrawRooms(Leaf leaf)
	{
		if (leaf.leftChild == null && leaf.rightChild == null)
		{
			for (int i = (int)leaf.room.x; i < leaf.room.xMax; i++)
			{
				for (int j = (int)leaf.room.y; j < leaf.room.yMax; j++)
				{
					GameObject instance = Instantiate(floorTile, new Vector3(i, j, 0f), Quaternion.identity);
					instance.transform.SetParent(transform);
					roomFloor[i, j] = instance;
				}
			}
		}
		else
		{
			DrawRooms(leaf.leftChild);
			DrawRooms(leaf.rightChild);
		}
	}

	public void DrawHallways(Leaf leaf)
	{
		if (leaf == null) { return; }

		DrawHallways(leaf.leftChild);
		DrawHallways(leaf.rightChild);

		foreach (Rect hallway in leaf.hallways)
		{
			for (int i = (int)hallway.x; i < hallway.xMax; i++)
			{
				for (int j = (int)hallway.y; j < hallway.yMax; j++)
				{
					if (roomFloor[i, j] == null)
					{
						GameObject instance = Instantiate(hallwayTile, new Vector3(i, j, 0f), Quaternion.identity);
						instance.transform.SetParent(transform);
						roomFloor[i, j] = instance;
					}
				}
			}
		}
	}

	public void DrawPowerUps()
    {

    }

}
