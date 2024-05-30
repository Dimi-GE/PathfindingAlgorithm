using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathfindingAlgorithm : MonoBehaviour
{
    private GridConstructor gridConstructor;
    public PathNode startNode;
    public PathNode endNode;

    public void StartFinding()
    {
        gridConstructor = GetComponent<GridConstructor>();

        ResetPath();

        if (startNode == null || endNode == null)
        {
            Debug.LogError("Start or End node is null. Check your positions.");
            return;
        }

        List<PathNode> path = FindPath(startNode, endNode);

        if (path != null)
        {
            foreach (PathNode node in path)
            {
                if (node != endNode)
                {
                    node.cubeInstance.GetComponent<Renderer>().material.color = Color.blue;
                }
                // Debug.Log("Path Node: " + node.position);
            }
        }
        else
        {
            Debug.LogError("Path not found.");
        }
    }

    List<PathNode> FindPath(PathNode start, PathNode end)
    {
        List<PathNode> openList = new List<PathNode> { start };
        HashSet<PathNode> closedList = new HashSet<PathNode>();

        start.gCost = 0;
        start.hCost = GetDistance(start, end);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == end)
            {
                return RetracePath(start, end);
            }

            foreach (PathNode neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.isWalkable || closedList.Contains(neighbor))
                {
                    continue;
                }

                float newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, end);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    List<PathNode> RetracePath(PathNode start, PathNode end)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    float GetDistance(PathNode a, PathNode b)
    {
        float dstX = Mathf.Abs(a.position.x - b.position.x);
        float dstZ = Mathf.Abs(a.position.z - b.position.z);

        if (dstX > dstZ)
            return 14 * dstZ + 10 * (dstX - dstZ);
        return 14 * dstX + 10 * (dstZ - dstX);
    }

    List<PathNode> GetNeighbors(PathNode node)
    {
        List<PathNode> neighbors = new List<PathNode>();
        Vector3[] neighborPositions = {
            new Vector3(1, 0, 0), new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1), new Vector3(0, 0, -1)
        };

        foreach (Vector3 pos in neighborPositions)
        {
            PathNode neighbor = gridConstructor.GetNodeAt(node.position + pos);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public void ResetPath()
    {
        foreach (PathNode node in gridConstructor.grid)
        {
            if (node != null && node != startNode && node != endNode)
            {
                if (node.isWalkable)
                {
                    Renderer renderer = node.cubeInstance.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = Color.white;
                    }
                }
            }
        }
    }
}
