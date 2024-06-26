using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class GridConstructor : MonoBehaviour
{
    int gridSize = 0;

    Vector3 OffsetX = new Vector3(1f, 0, 0);
    Vector3 OffsetY = new Vector3(0, 0, 1f);

    public Camera mainCamera;

    public PathNode[,] grid;

    // List to store references to the instantiated Cube instances
    private List<GameObject> cubeInstances = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConstructGrid()
    {
        // Load the prefab from the Resources folder
        GameObject Cube = Resources.Load<GameObject>("Cube");

        if (cubeInstances.Count > 0)
        {
            RemoveExistingObjects();
        }

        if (gridSize < 0 || gridSize > 50)
        {
            return;
        }

        // Check if the prefab was successfully loaded
        if (Cube != null)
        {
            // Reference to the GameObject this script is attached to
            GameObject ownerGameObject = this.gameObject;

            grid = new PathNode[gridSize, gridSize];

            Vector3 startPosition = CalculateGridPosition();

            // Calculate total height of the grid
            // float totalHeight = gridSize * OffsetY.z * -1;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    // Calculate the position of the Cube instance
                    Vector3 position = startPosition + OffsetX * i + OffsetY * j;

                    // Instantiate the Cube prefab at the calculated position
                    GameObject cubeInstance = Instantiate(Cube, position, Quaternion.identity);

                    // Add the instantiated Cube instance to the list
                    cubeInstances.Add(cubeInstance);

                    grid[i, j] = new PathNode(position, true, cubeInstance); // Initialize as walkable
                }
            }
        }
        else
        {
            Debug.LogError("Cube Prefab could not be loaded from Resources.");
        }
    }

    public void SetGridSize(string GridSize)
    {
        gridSize = int.Parse(GridSize);
    }

    public void RemoveExistingObjects()
    {
        foreach (GameObject cubeInstance in cubeInstances)
        {
            Destroy(cubeInstance);
            ObstaclesHandler obstaclesHandler = this.gameObject.GetComponent<ObstaclesHandler>();
            if (obstaclesHandler != null)
            {
                obstaclesHandler.Index = 0;
            }
        }
        // Clear the list of Cube instances
        cubeInstances.Clear();
    }

    public PathNode GetNodeAt(Vector3 position)
    {
        foreach (PathNode node in grid)
        {
            if (node.position == position)
            {
                // Debug.LogWarning("Node position: " + node.position);
                return node;
            }
        }
        // Debug.LogWarning("Node not found at position: " + position);
        return null;
    }

    private Vector3 CalculateGridPosition()
    {
        GameObject ownerGameObject = this.gameObject;
        // Calculate the size of the grid in world units
        float gridWidth = (gridSize - 1) * OffsetX.x;
        float gridHeight = (gridSize - 1) * OffsetY.z;

        // Calculate the camera's visible area in world units
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        // Calculate the starting position to center the grid in the camera's view
        Vector3 startPosition = new Vector3(
            mainCamera.transform.position.x - gridWidth / 2,
            ownerGameObject.transform.position.y,
            mainCamera.transform.position.z - gridHeight / 2
        );

        return startPosition;
    }
}
