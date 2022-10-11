using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int Coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node ConnectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        Coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}