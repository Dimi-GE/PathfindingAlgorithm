using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;
    public float rotationSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 50f;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a direction vector based on the input axes
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

        // Transform the direction vector from local space to world space
        direction = transform.TransformDirection(direction);

        // Move the camera in the transformed direction
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoom = scroll * zoomSpeed * Time.deltaTime;

        // Modify the field of view for zooming in/out
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoom, minZoom, maxZoom);
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 dragDirection = Input.mousePosition - dragOrigin;
            float rotationX = dragDirection.y * rotationSpeed * Time.deltaTime;
            float rotationY = dragDirection.x * rotationSpeed * Time.deltaTime;

            // Apply pitch (rotation around X axis)
            transform.Rotate(Vector3.right, rotationX, Space.Self);

            // Apply yaw (rotation around Y axis)
            transform.Rotate(Vector3.up, rotationY, Space.World);

            dragOrigin = Input.mousePosition;
        }
    }
}
