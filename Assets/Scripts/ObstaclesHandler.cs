using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHandler : MonoBehaviour
{
    bool isMarkingObstaclesAllowed = false;
    bool isMarkingStartEndPointsAllowed = false;
    public Camera mainCamera;
    public int Index = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMarkingObstaclesAllowed(bool Marking)
    {
        isMarkingObstaclesAllowed = Marking;
    }

    public void SetMarkingStartEndPointsAllowed(bool Marking)
    {
        isMarkingStartEndPointsAllowed = Marking;
    }

    public void markAsObstacle()
    {
        // Perform a raycast from the mouse cursor position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MarkableObject MarkableObjectRef = hit.collider.gameObject.GetComponent<MarkableObject>();
            if (MarkableObjectRef != null)
            {
                MarkableObjectRef.Obstacle();
            }
        }

    }

    public void resetMarks()
    {
        // Perform a raycast from the mouse cursor position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MarkableObject MarkableObjectRef = hit.collider.gameObject.GetComponent<MarkableObject>();
            if (MarkableObjectRef != null)
            {
                if (MarkableObjectRef.Index == 3)
                {
                    Index = 1;
                    MarkableObjectRef.Traversable();
                }
                else if (MarkableObjectRef.Index == 2)
                {
                    Index = 0;
                    MarkableObjectRef.Traversable();
                }
                else
                {
                    MarkableObjectRef.Traversable();
                }
                
            }
        }
    }

    public void markAsStartPoint()
    {
        // Perform a raycast from the mouse cursor position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MarkableObject MarkableObjectRef = hit.collider.gameObject.GetComponent<MarkableObject>();
            if (Index == 0 && MarkableObjectRef != null)
            {
                Index++;
                MarkableObjectRef.StartPoint();
            }
            else if (Index == 1 && MarkableObjectRef != null)
            {
                Index++;
                MarkableObjectRef.EndPoint();
            }
        }
    }

    public void HandleMarking()
    {
        if (isMarkingObstaclesAllowed)
        {
            markAsObstacle();
        }
        else if(isMarkingStartEndPointsAllowed)
        {
            markAsStartPoint();
        }
    }
}
