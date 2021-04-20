using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
	[Header("DUNGEON VARIABLES")]
	public DungeonData dungeon;
	private int DUNGEON_W;
	private int DUNGEON_H;
	private int MIN_ROOM_SIZE;
	public GameObject floorTile;
	public GameObject hallwayTile;
	public GameObject wallTile;
	public GameObject bounds;

	[Header("CHARACTERS")]
	public GameObject player;
	public GameObject [] enemies;

	private GameObject[,] roomFloor;

	public List<Rect> rooms;

	/*** SPAWN ENEMY VARIABLES ***/
	private Spawner spawner;
	private GameObject[] spawnedEnemies;
	private List<List<int>> enemiesInRooms;

	/*** ITEMS***/
	[Header("ITEMS")]
	public GameObject[] items;

	/*** TRAPS***/
	[Header("TRAPS")]
	public List<GameObject> traps;

	void Start()
	{
		DUNGEON_W = dungeon.dungeonWidth;
		DUNGEON_H = dungeon.dungeonHeight;
		MIN_ROOM_SIZE = dungeon.minSizeRoom;
		dungeon.rooms = new List<Rect>();
		dungeon.hallways = new List<Rect>();
		rooms = dungeon.rooms;
		Leaf root = new Leaf(new Rect(0, 0, DUNGEON_W, DUNGEON_H), dungeon);
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

		DrawBreakable();

		DrawTraps();

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
		/*
		foreach (GameObject item in items)
        {
			Instantiate(item, new Vector3(x, Random.Range(temp.y + 1 + i, temp.y + temp.height - 1 - i), 0f), Quaternion.identity);
			i++;
		}*/
		GameObject smallchest = Instantiate(items[4], new Vector3(x, Random.Range(temp.y + 1, temp.y + temp.height - 1), 0f), Quaternion.identity);
		GameObject bigchest = Instantiate(items[5], new Vector3(x, Random.Range(temp.y + 1 + 2, temp.y + temp.height - 1 - 2), 0f), Quaternion.identity);

		//Instantiate(traps[0], new Vector3(temp.x, temp.yMax - 0.5f, 0f), Quaternion.identity);
		//Instantiate(traps[1], new Vector3(temp.x, temp.yMax - 0.5f - 0.5f, 0f), Quaternion.identity);



		//float x2 = Random.Range(temp.x + 1, temp.x + temp.width - 1);
		//float y2 = Random.Range(temp.y + 1, temp.y + temp.height - 1);

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
						instance.gameObject.tag = "Hall";
						instance.transform.SetParent(transform);
						roomFloor[i, j] = instance;
					}
				}
			}

			int n = Random.Range(0, 1);
			if (n == 1)
            {
				int trap = Random.Range(0, 1);
				if(trap == 0) { Instantiate(traps[0], new Vector3(hallway.xMax/2, hallway.yMax/2 - 0.5f, 0f), Quaternion.identity); }
                else { Instantiate(traps[1], new Vector3(hallway.xMax/2, hallway.xMax/2 - 0.5f, 0f), Quaternion.identity); }
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
				}
				 

			}





			//}
			//}
		}
    }

	public void DrawTraps()
    {
		for(int i = 0; i < dungeon.hallways.Count; i += 3)
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
