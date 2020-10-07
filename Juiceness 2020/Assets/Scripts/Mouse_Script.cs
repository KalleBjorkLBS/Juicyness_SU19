using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Script : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; //Just Z axis so its getting the position infront of the camera
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //Gets mouse postion in world space
        transform.position = mousePosition; //Object following the mouse position
    }
}
