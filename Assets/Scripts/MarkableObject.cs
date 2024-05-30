using UnityEngine;

public class MarkableObject : MonoBehaviour
{
    public int Index;
    UnityEngine.Color obstacleColor = UnityEngine.Color.gray;
    UnityEngine.Color traversableleColor = UnityEngine.Color.white;
    UnityEngine.Color startPoint = UnityEngine.Color.green;
    UnityEngine.Color endPoint = UnityEngine.Color.red;

    private GridConstructor gridConstructor;
    private PathfindingAlgorithm pathfindingAlgorithm;

    // Start is called before the first frame update
    void Start()
    {
        gridConstructor = FindObjectOfType<GridConstructor>();
        pathfindingAlgorithm = FindObjectOfType<PathfindingAlgorithm>();
        Traversable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Obstacle()
    {
        Index = 0;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = obstacleColor;
        }

        // Get the corresponding PathNode and mark it as not walkable
        PathNode node = gridConstructor.GetNodeAt(transform.position);
        if (node != null)
        {
            node.isWalkable = false;
        }
    }

    public void Traversable()
    {
        Index = 1;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = traversableleColor;
        }

        // Get the corresponding PathNode and mark it as not walkable
        PathNode node = gridConstructor.GetNodeAt(transform.position);
        if (node != null)
        {
            node.isWalkable = true;
        }
    }

    public void StartPoint()
    {
        Index = 2;
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.color = startPoint;
        }

        PathNode node = gridConstructor.GetNodeAt(transform.position);
        if (node != null)
        {
            pathfindingAlgorithm.startNode = node;
        }

    }

    public void EndPoint()
    {
        Index = 3;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = endPoint;
        }

        PathNode node = gridConstructor.GetNodeAt(transform.position);
        if (node != null)
        {
            pathfindingAlgorithm.endNode = node;
        }
    }
}
