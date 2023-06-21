using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMainCamera : MonoBehaviour
{
    public Transform objectPosition;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    private void Start()
    {
        xOffset = 0;
        yOffset = 0.2f;
        zOffset = -1.9f;
    }

    void Update()
    {
        //Place the camera at a certain distance from the car to view the car
        transform.position = objectPosition.transform.position + new Vector3(xOffset, yOffset, zOffset);
    }
}
