using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPTree
{
	/*
	 * The Split function will be called with all the nodes, in case it return true we'll call recursively to the function
	 */
	public void CreateTree(Leaf leaf, int minRoomSize)
	{
		if (leaf.Split(minRoomSize))
		{
			CreateTree(leaf.leftChild, minRoomSize);
			CreateTree(leaf.rightChild, minRoomSize);
		}
	}
}
