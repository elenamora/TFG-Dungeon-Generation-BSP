using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPTree
{
	public void CreateTree(Leaf leaf, int minRoomSize)
	{
		if (leaf.Split(minRoomSize))
		{
			CreateTree(leaf.leftChild, minRoomSize);
			CreateTree(leaf.rightChild, minRoomSize);
		}
	}
}
