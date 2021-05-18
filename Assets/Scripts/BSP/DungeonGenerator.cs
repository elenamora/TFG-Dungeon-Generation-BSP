using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
	[Header("DUNGEON VARIABLES")]
	public DungeonData dungeon;
	public GameData gameData;
	public GameObject floorTile;
	public GameObject hallwayTile;
	public GameObject wallTile;
	public GameObject bounds;

	/*** DUNGEON VARIABLES ***/
	private GameObject[,] roomFloor;
	private List<Rect> rooms;

	[Header("ENEMY SPAWNER")]
	public EnemyManager enemyManager;
	/*** SPAWN ENEMY VARIABLES ***/
	private EnemySpawner spawner;
	private GameObject[] spawnedEnemies;
	private List<List<int>> enemiesInRooms;

	[Header("CHARACTERS")]
	public GameObject player;
	public GameObject [] enemies;

	/*** ITEMS ***/
	[Header("ITEMS")]
	public ItemManager itemManager;
	public GameObject[] items;

	/*** TRAPS ***/
	[Header("TRAPS")]
	public List<GameObject> traps;

	void Start()
	{
		/* Dungeon Creation */
		dungeon.ResetDungeon();
		enemyManager.ResetEnemies();
		itemManager.ResetItems();
		rooms = dungeon.rooms;
		Leaf root = new Leaf(new Rect(0, 0, dungeon.dungeonWidth, dungeon.dungeonHeight), dungeon);
		BSPTree tree = new BSPTree();
		tree.CreateTree(root, dungeon.minSizeRoom);
		root.CreateRoom(dungeon.minSizeRoom);

		roomFloor = new GameObject[dungeon.dungeonWidth, dungeon.dungeonHeight];
		DrawRooms(root);
		DrawHallways(root);
		DrawWalls(root);
		DrawBounds(root);

		/* Elements instances */
		spawner = new EnemySpawner(rooms);
		DrawPlayer();
		DrawChests();
		DrawEnemies();

		DrawBreakable();
		DrawTraps();

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

		foreach (Rect hallway in dungeon.hallways)
		{
			for (int i = (int)hallway.x; i < hallway.xMax; i++)
			{
				for (int j = (int)hallway.y; j < hallway.yMax; j++)
				{
					if (roomFloor[i, j] == null)
					{
						GameObject instance = Instantiate(hallwayTile, new Vector3(i, j, 0f), Quaternion.identity);
						instance.gameObject.tag = "Hall";
						instance.transform.SetParent(transform);
						roomFloor[i, j] = instance;
					}
				}
			}
		}
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
		Rect temp = rooms[0];
		float x = Random.Range(temp.x + 1, temp.x + temp.width - 1);
		float y = Random.Range(temp.y + 1, temp.y + temp.height - 1);

		player = Instantiate(player, new Vector3(x, y, 0f), Quaternion.identity);
	}

	public void DrawChests()
    {
		// Instantiate the small chest in a random room between the first and the last room
		int index;
		if (rooms.Count > 5) index = Random.Range(2, rooms.Count - 1);
		else index = 1;

		Rect temp = rooms[index];
		Instantiate(items[4], temp.center, Quaternion.identity);

		// Instantiate the big in the last room
		Instantiate(items[5], rooms[rooms.Count - 1].center, Quaternion.identity);
	}

	public void DrawEnemies()
    {
        enemiesInRooms = spawner.EnemiesInRooms();

		spawnedEnemies = new GameObject[spawner.numOfEnemies];

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
						enemyManager.initialEnemies.Add(0);
						break;

					case 1:
						spawnedEnemies[numEnem] = Instantiate(enemies[1], new Vector3(x, y, 0f), Quaternion.identity);
						enemyManager.initialEnemies.Add(1);
						numEnem++;
						break;

					case 2: 
						spawnedEnemies[numEnem] = Instantiate(enemies[2], new Vector3(x, y, 0f), Quaternion.identity);
						enemyManager.initialEnemies.Add(2);
						numEnem++;
						break;
				}
			}
		}
	}

	public void DrawBreakable()
    {
		foreach (Rect room in rooms)
		{
			//for (int i = (int)hall.x; i < hall.xMax; i++)
			//{
				//for (int j = (int)hall.y; j < hall.yMax; j++)
				//{
			int num = Random.Range(1, (int)(room.width * room.height * 0.15));

			for ( int i = 0; i < num; i++)
            {
				List<Vector3> possiblePositions = new List<Vector3>();

				Vector3 posx1 = new Vector3(Random.Range(room.x, room.xMax - .5f), room.y, 0f);

				//if (!roomFloor[(int)posx1.x, (int)(posx1.y - 2f)].CompareTag("Hall")){
				possiblePositions.Add(posx1);
                //}

				Vector3 posx2 = new Vector3(Random.Range(room.x, room.xMax- .5f), room.yMax - .5f, 0f);

				//if (!roomFloor[(int)posx1.x, (int)(posx1.y + 2)].CompareTag("Hall"))
				//{
				possiblePositions.Add(posx2);
				//}

				Vector3 posy1 = new Vector3(room.x, Random.Range(room.y, room.yMax - .5f), 0f);

				//if (!roomFloor[(int)(posx1.x - 2), (int)posx1.y].CompareTag("Hall"))
				//{
				possiblePositions.Add(posy1);
				//}

				Vector3 posy2 = new Vector3(room.xMax - .5f, Random.Range(room.y, room.yMax - .5f), 0f);

				//if (!roomFloor[(int)(posx1.x + 2), (int)posx1.y].CompareTag("Hall"))
				//{
				possiblePositions.Add(posy2);
				//}



				/*
				foreach (Vector3 pos in possiblePositions)
				{
					if (roomFloor[(int)pos.x, (int)(pos.y - 1.5f)].CompareTag("Hall")
					|| roomFloor[(int)pos.x, (int)(pos.y + 1.5f)].CompareTag("Hall")
					|| roomFloor[(int)(pos.x - 1.5f), (int)(pos.y)].CompareTag("Hall")
					|| roomFloor[(int)(pos.x + 1.5f), (int)(pos.y)].CompareTag("Hall"))
                    {
						possiblePositions.Remove(pos);
                    }

				}*/

				if (possiblePositions != null)
                {
					Vector3 pos = possiblePositions[Random.Range(0, possiblePositions.Count)];

					Instantiate(items[3], pos, Quaternion.identity);
					itemManager.initialItems += 1;
				}
				 

			}

			//}
			//}
		}
    }

	public void DrawTraps()
    {
		for(int i = 1; i < dungeon.hallways.Count; i += 3)
		{
			Rect r = dungeon.hallways[i];
			float x, y;
            if (r.width < 4 && r.height < 4)
            {
				x = r.x + 0.2f;
				y = r.y + 0.2f;
            }
            else
            {
				x = r.center.x - 0.45f;
				y = r.center.y - 0.45f;
			}

			int t = Random.Range(0, 2);
			if (t == 0) { Instantiate(traps[0], new Vector3(x, y , 0f), Quaternion.identity); }
			else { Instantiate(traps[1], new Vector3(x, y, 0f), Quaternion.identity); }
        }
    }

}
