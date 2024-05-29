using UnityEngine;

public class MarkableObject : MonoBehaviour
{
    public int Index;
    UnityEngine.Color obstacleColor = UnityEngine.Color.gray;
    UnityEngine.Color traversableleColor = UnityEngine.Color.white;
    UnityEngine.Color startPoint = UnityEngine.Color.green;
    UnityEngine.Color endPoint = UnityEngine.Color.red;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void Traversable()
    {
        Index = 1;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = traversableleColor;
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
    }

    public void EndPoint()
    {
        Index = 3;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = endPoint;
        }
    }
}
