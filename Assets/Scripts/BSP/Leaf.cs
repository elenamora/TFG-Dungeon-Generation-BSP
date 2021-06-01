using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
	public Leaf leftChild, rightChild;
	public Rect rect;
	public Rect room;

	public DungeonData data;

	public Leaf(Rect mrect, DungeonData data)
	{
		rect = mrect;
		this.data = data;
	}

	/*
	 * Method that will split recursively the initial dungeon until we reach a minsize and cannot split anymore
	 * @param: minRoomSize The minimum size that a room can have
	 * Output: Boolean that will indicate if we can split or not a given area
	 */
	public bool Split(int minRoomSize)
	{
		// If the node we want to split has a child it isn't a leaf which means we've already split it
		if (leftChild != null || rightChild != null)
		{
			return false;
		}

		// MinSizeRoom = 6 => 3x3(widthxheight)
		// Choose the minimum between width and height. If that minimum is less than the minRoomSize we don't split
		if (Mathf.Min(rect.width, rect.height) / 2 < minRoomSize) { return false; }

		// Select randomly if split vertically or horizontally
		bool splitH = Random.Range(0, 1) > 2;

		// If the width is larger than the height by a 25% the split will be vertical
		if (rect.width / rect.height >= 1.25) { splitH = false; }

		// If the height is larger than the width by a 25% the split will be horizontal
		else if (rect.height / rect.width >= 1.25) { splitH = true; }

		int split;

		if (splitH)
		{
			// Random position between the minRoomSize and the width - minRoomSize
			split = (int)Random.Range(minRoomSize, rect.width - minRoomSize);
			leftChild = new Leaf(new Rect(rect.x, rect.y, rect.width, split), data);
			rightChild = new Leaf(new Rect(rect.x, rect.y + split, rect.width, rect.height - split), data);
		}
		else
		{
			// Random position between the minRoomSize and the height - minRoomSize
			split = (int)Random.Range(minRoomSize, rect.height - minRoomSize);
			leftChild = new Leaf(new Rect(rect.x, rect.y, split, rect.height), data);
			rightChild = new Leaf(new Rect(rect.x + split, rect.y, rect.width - split, rect.height), data);
		}


		return true;
	}

	/*
	 * Function called to create rooms inside each leaf node
	 * @param: minRoomSize The minimum size that a room can hav
	 */
	public void CreateRoom(int minRoomSize)
	{
		// If there exists a child we'll recursively call this function since the rooms have to be drawn inside de leaf nodes
		if (leftChild != null) { leftChild.CreateRoom(minRoomSize); }

		if (rightChild != null) { rightChild.CreateRoom(minRoomSize); }

		if (leftChild != null && rightChild != null) { CreateHallways(leftChild.GetRoom(), rightChild.GetRoom()); }

		// If there aren't any children it means it is a leaf node so we can create a room
		if (leftChild == null && rightChild == null)
		{
			float roomWidth, roomHeight, roomX, roomY;

			roomWidth = Random.Range(minRoomSize / 2, rect.width - 2);
			roomHeight = Random.Range(minRoomSize / 2, rect.height - 2);

			roomX = Random.Range(1, rect.width - roomWidth - 1);
			roomY = Random.Range(1, rect.height - roomHeight - 1);

			// room position will be absolute
			room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
			data.rooms.Add(room);
		}
	}

	public Rect GetRoom()
	{
		if (leftChild != null) { return room = leftChild.GetRoom(); }

		if (rightChild != null) { return room = rightChild.GetRoom(); }

		return room;
	}

	/*
	 * Function that will create a hallway between two rooms
	 * @param: leftRoom Rectangle that represents the left room
	 * @param: rightRoom Rectangle that represents the right room
	 */
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
				data.hallways.Add(new Rect(leftPosition.x, leftPosition.y, Mathf.Abs(w), 1));

				if (h < 0) {
					data.hallways.Add(new Rect(rightPosition.x, leftPosition.y, 1, Mathf.Abs(h)));
				}

				else {
					data.hallways.Add(new Rect(rightPosition.x, leftPosition.y, 1, -Mathf.Abs(h)));
				}
			}

			else
			{
				if (h < 0) {
					data.hallways.Add(new Rect(leftPosition.x, leftPosition.y, 1, Mathf.Abs(h)));
				}

				else {
					data.hallways.Add(new Rect(leftPosition.x, rightPosition.y, 1, Mathf.Abs(h)));
				}

				data.hallways.Add(new Rect(leftPosition.x, rightPosition.y, Mathf.Abs(w), 1));
			}
		}
		else
		{
			if (h < 0) {
				data.hallways.Add(new Rect(leftPosition.x, leftPosition.y, 1, Mathf.Abs(h)));
			}

			else {
				data.hallways.Add(new Rect(rightPosition.x, rightPosition.y, 1, Mathf.Abs(h)));
			}
		}

	}

}
