using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject PathfindingAlgorithmManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            if (PathfindingAlgorithmManager != null)
            {
                PathfindingAlgorithmManager.GetComponent<ObstaclesHandler>().HandleMarking();
            }
        }

        // Check if the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            if (PathfindingAlgorithmManager != null)
            {
                PathfindingAlgorithmManager.GetComponent<ObstaclesHandler>().resetMarks();
            }
        }
    }
}
