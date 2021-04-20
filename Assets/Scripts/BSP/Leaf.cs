using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
	public Leaf leftChild, rightChild;
	public Rect rect;
	public Rect room;

	public List<Rect> hallways = new List<Rect>();
	public DungeonData data;

	public Leaf(Rect mrect, DungeonData data)
	{
		rect = mrect;
		this.data = data;
	}

	public bool Split(int minRoomSize)
	{
		if (leftChild != null || rightChild != null)
		{
			return false;
		}

		bool splitH = Random.Range(0, 1) > 2;

		if (rect.width / rect.height >= 1.25) { splitH = false; }

		else if (rect.height / rect.width >= 1.25) { splitH = true; }

		if (Mathf.Min(rect.width, rect.height) / 2 < minRoomSize) { return false; }

		int split;

		if (splitH)
		{
			split = (int)Random.Range(minRoomSize, rect.width - minRoomSize);
			leftChild = new Leaf(new Rect(rect.x, rect.y, rect.width, split), data);
			rightChild = new Leaf(new Rect(rect.x, rect.y + split, rect.width, rect.height - split), data);
		}
		else
		{
			split = (int)Random.Range(minRoomSize, rect.height - minRoomSize);
			leftChild = new Leaf(new Rect(rect.x, rect.y, split, rect.height), data);
			rightChild = new Leaf(new Rect(rect.x + split, rect.y, rect.width - split, rect.height), data);
		}


		return true;
	}

	public void CreateRoom(int minRoomSize, List<Rect> rooms)
	{
		if (leftChild != null) { leftChild.CreateRoom(minRoomSize, rooms); }

		if (rightChild != null) { rightChild.CreateRoom(minRoomSize, rooms); }

		if (leftChild != null && rightChild != null) { CreateHallways(leftChild.GetRoom(), rightChild.GetRoom()); }

		if (leftChild == null && rightChild == null)
		{
			float roomWidth, roomHeight, roomX, roomY;

			roomWidth = Random.Range(minRoomSize / 2, rect.width - 2);
			roomHeight = Random.Range(minRoomSize / 2, rect.height - 2);

			roomX = Random.Range(1, rect.width - roomWidth - 1);
			roomY = Random.Range(1, rect.height - roomHeight - 1);

			// room position will be absolute
			room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
			//rooms.Add(room);
			data.rooms.Add(room);
		}
	}

	public Rect GetRoom()
	{
		if (leftChild != null) { return room = leftChild.GetRoom(); }

		if (rightChild != null) { return room = rightChild.GetRoom(); }

		return room;
	}

	public void CreateHallways(Rect leftRoom, Rect rightRoom)
    {
		
		Vector2 leftPosition = new Vector2(Random.Range(leftRoom.x + 1, leftRoom.xMax - 1), Random.Range(leftRoom.y + 1, leftRoom.yMax - 1));
		Vector2 rightPosition = new Vector2(Random.Range(rightRoom.x + 1, rightRoom.xMax - 1), Random.Range(rightRoom.y + 1, rightRoom.yMax - 1));

		// We make sure that the leftPosition is actually on the left, if not we change the positions
		if (leftPosition.x > rightPosition.x)
		{
			Vector2 temp = leftPosition;
			leftPosition = rightPosition;
			rightPosition = temp;
		}

		// We compute the width and height between positions so we know the size for the rect doing of hallway
		float w = leftPosition.x - rightPosition.x;
		float h = leftPosition.y - rightPosition.y;

		// LeftPosition and RightPosition are not align horizontally
		if (w != 0)
		{
			// We choose randomly to go horizontal then vertical or the opposite
			if (Random.Range(0, 1) > 2)
			{
				Rect r = new Rect(leftPosition.x, leftPosition.y, Mathf.Abs(w), 1);
				hallways.Add(r);
				data.hallways.Add(r);

				if (h < 0) {
					Rect re = new Rect(rightPosition.x, leftPosition.y, 1, Mathf.Abs(h));
					hallways.Add(re);
					data.hallways.Add(re);
				}

				else {
					Rect re = new Rect(rightPosition.x, leftPosition.y, 1, -Mathf.Abs(h));
					hallways.Add(re);
					data.hallways.Add(re);
				}
			}

			else
			{
				if (h < 0) {
					Rect r = new Rect(leftPosition.x, leftPosition.y, 1, Mathf.Abs(h));
					hallways.Add(r);
					data.hallways.Add(r);
				}

				else {
					Rect r = new Rect(leftPosition.x, rightPosition.y, 1, Mathf.Abs(h));
					hallways.Add(r);
					data.hallways.Add(r);
				}

				Rect re = new Rect(leftPosition.x, rightPosition.y, Mathf.Abs(w), 1);
				hallways.Add(re);
				data.hallways.Add(re);
			}
		}
		else
		{
			if (h < 0) {
				Rect r = new Rect(leftPosition.x, leftPosition.y, 1, Mathf.Abs(h));
				hallways.Add(r);
				data.hallways.Add(r);
			}

			else {
				Rect r = new Rect(rightPosition.x, rightPosition.y, 1, Mathf.Abs(h));
				hallways.Add(r);
				data.hallways.Add(r);
			}
		}

	}

	public List<Rect> GetHallways()
    {
		return hallways;
    }

}
