using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.UI;
using TMPro;

// - Script for the PlayerCamera and Interactions with Objects/Npc's
// - Updates Position using MoveCamera script
// - Daniel Bruijn

public class PlayerCam : MonoBehaviour
{
    // - Variables
    // - Public
    [Header("Sensitivity")]
    public float MouseSens;

    [Header("Refrences")]
    public Transform orientation;
    public Transform camHolder;
    
    [Header("Interaction")]
    public LayerMask interactionLayer;
    public float interactDistance = 1.5f;
    public TMP_Text interactionText;

    // - Private
    private float xRotation;
    private float yRotation;
    
    private void Start()
    {
        // - Locks Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // - Sets InteractionText false
        interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleCameraRotation();
        PlayerRaycast();
    }
    
    
    private void HandleCameraRotation()
        {
            // - Get mouse Input.
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * MouseSens;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * MouseSens;
    
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    
            // - Rotate cam and Orientation
            camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    
        private void PlayerRaycast()
        {
            // - Shoots Raycast out using the InteractionDistance
            Ray ray = new Ray(camHolder.position, camHolder.forward);
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
    
            // - Checks if the RayCast hit an interaction Layer
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactionLayer))
            {
                interactionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(hit.collider.name);
                }
            }
            else
            {
                // - Sets interactionText to false if it is active
                if (interactionText.gameObject.activeSelf) interactionText.gameObject.SetActive(false);
            }
        }

    public void DoFov(float endValue)
    {
        // - Sets the FOV for the Camera using DOTween
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        // - Sets the Camera Tilt using DOTween
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}