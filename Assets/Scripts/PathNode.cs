using UnityEngine;

public class PathNode
{
    public Vector3 position; // Position of the node
    public bool isWalkable; // Whether the node is walkable or not
    public PathNode parent; // Parent node in the path
    public float gCost; // Cost from start node
    public float hCost; // Heuristic cost to end node
    public GameObject cubeInstance;

    // Constructor
    public PathNode(Vector3 pos, bool walkable, GameObject cubeInstance)
    {
        position = pos;
        isWalkable = walkable;
        parent = null;
        gCost = float.MaxValue; // Set to max value initially
        hCost = 0;
        this.cubeInstance = cubeInstance;
    }

    // Property to calculate total cost (fCost)
    public float fCost { get { return gCost + hCost; } }
}
