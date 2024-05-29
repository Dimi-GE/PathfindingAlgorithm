using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour
{
    public TMP_InputField gridSize;
    public GameObject PathfindingAlgorithmManager;

    public Toggle MarkingObstacles;
    public Toggle MarkingStartEndPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParseGridSize()
    {
        string parsedGridSize = gridSize.text;

        if (PathfindingAlgorithmManager != null)
        {
            if (string.IsNullOrEmpty(parsedGridSize))
            {
                Debug.Log("The text is empty");
            }
            else
            {
                PathfindingAlgorithmManager.GetComponent<GridConstructor>().SetGridSize(parsedGridSize);
            }
        }
    }

    public void ToggleMarkingObstacles()
    {
        if (PathfindingAlgorithmManager != null) 
        {
            if (MarkingObstacles.isOn)
            {
                MarkingStartEndPoints.interactable = false;
                PathfindingAlgorithmManager.GetComponent<ObstaclesHandler>().SetMarkingObstaclesAllowed(MarkingObstacles.isOn);
            }
            else
            {
                MarkingStartEndPoints.interactable = true;
                PathfindingAlgorithmManager.GetComponent<ObstaclesHandler>().SetMarkingObstaclesAllowed(MarkingObstacles.isOn);
            }
        }
    }

    public void ToggleMarkingStartEndPoints()
    {
        if (PathfindingAlgorithmManager != null)
        {
            if (MarkingStartEndPoints.isOn)
            {
                MarkingObstacles.interactable = false;
                PathfindingAlgorithmManager.GetComponent<ObstaclesHandler>().SetMarkingStartEndPointsAllowed(MarkingStartEndPoints.isOn);
            }
            else
            {
                MarkingObstacles.interactable = true;
                PathfindingAlgorithmManager.GetComponent<ObstaclesHandler>().SetMarkingStartEndPointsAllowed(MarkingStartEndPoints.isOn);
            }
            
        }
    }    

}
