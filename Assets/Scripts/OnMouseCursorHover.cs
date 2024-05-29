using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseCursorHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Debug.Log("Mouse position is changed.");
        }
    }
}
