using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// - Simple Script to update Camera Position
// - Daniel Bruijn

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}