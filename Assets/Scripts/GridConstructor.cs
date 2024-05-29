using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridConstructor : MonoBehaviour
{
    int gridSize = 0;

    Vector3 OffsetX = new Vector3(0.4f, 0, 0);
    Vector3 OffsetY = new Vector3(0, 0, 0.4f);

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

            // Calculate total height of the grid
            float totalHeight = gridSize * OffsetY.z * -1;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    // Calculate the position of the Cube instance
                    Vector3 position = ownerGameObject.transform.position + OffsetX * i + OffsetY * j;

                    // Instantiate the Cube prefab at the calculated position
                    GameObject cubeInstance = Instantiate(Cube, position, Quaternion.identity);
                    
                    // Set the cube's parent to the specified parent object
                    cubeInstance.transform.SetParent(ownerGameObject.transform, true);

                    // Move the Parent object by position of each Cube added devided by 2
                    ownerGameObject.transform.position = (ownerGameObject.transform.position - cubeInstance.transform.position) / 2;

                    // Center the parent GameObject vertically based on the grid's total height
                    ownerGameObject.transform.position = new Vector3(ownerGameObject.transform.position.x, totalHeight / 1.25f, ownerGameObject.transform.position.z);

                    // Add the instantiated Cube instance to the list
                    cubeInstances.Add(cubeInstance);
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
        }
        // Clear the list of Cube instances
        cubeInstances.Clear();
    }
}
