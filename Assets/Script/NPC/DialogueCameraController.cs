using UnityEngine;
using System.Collections;

// - Dialogue Camera Controller
// - Handles camera moving and returning
// - Daniel Bruijn

public class DialogueCameraController : MonoBehaviour
{
    // - Variables
    public static DialogueCameraController Instance;

    [Header("Camera References")]
    public Transform camHolder;
    public PlayerCam playerCamScript;
    public MoveCamera moveCameraScript;
    public PlayerController playerControllerScript;

    [Header("Transition Speed")]
    public float transitionSpeed = 5f;

    [Header("Is Active")]
    public bool dialogueActive = false;
    
    // - Private
    Vector3 originalPosition;
    Quaternion originalRotation;
    bool hasStoredOriginal = false;

    Vector3 targetPosition;
    Quaternion targetRotation;

    bool moving = false;
    bool returningToPlayer = false;
    
    Transform followTarget;
    bool following = false;
    

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (following && followTarget != null)
        {
            camHolder.position = Vector3.Lerp(
                camHolder.position,
                followTarget.position,
                Time.deltaTime * transitionSpeed
            );

            camHolder.rotation = Quaternion.Lerp(
                camHolder.rotation,
                followTarget.rotation,
                Time.deltaTime * transitionSpeed
            );

            return;
        }
        
        // - Camera moving return
        if (!moving) return;

        // - Sets the camHolder postion
        camHolder.position = Vector3.Lerp(
            camHolder.position,
            targetPosition,
            Time.deltaTime * transitionSpeed
        );

        // - Sets the camHolder rotation
        camHolder.rotation = Quaternion.Lerp(
            camHolder.rotation,
            targetRotation,
            Time.deltaTime * transitionSpeed
        );

        // - change the camera position and rotation
        if (Vector3.Distance(camHolder.position, targetPosition) < 0.01f)
        {
            Debug.Log("Final Camera Pos BEFORE restore: " + camHolder.position);
            
            camHolder.position = targetPosition;
            camHolder.rotation = targetRotation;
            moving = false;
            
            // restore controls AFTER movement is done
            if (returningToPlayer)
                RestoreControlsInstant();
        }
    }

    public void FocusOnNPC(Transform npc)
    {
        // - Disables the PlayerCam,MoveCam and PlayerController scripts to fix fighting
        playerCamScript.enabled = false;
        moveCameraScript.enabled = false;
        playerControllerScript.enabled = false;
        
        if (!hasStoredOriginal)
        {
            // - Sets the new positions
            originalPosition = camHolder.position;
            originalRotation = camHolder.rotation;
            hasStoredOriginal = true;
            Debug.Log("Stored Original: " + originalPosition);
        }

        // - Finds the Anchor Point for Camera Position
        Transform anchor = npc.Find("DialogueCameraAnchor_Obj");
        if (anchor == null)
        {
            Debug.LogError("CameraAnchor missing on NPC!");
            return;
        }
        targetPosition = anchor.position;
        targetRotation = anchor.rotation;

        // - Sets moving and return to true when transition is active
        moving = true;
    }

    public void ReturnCamera()
    {
        // - Stops the follwing for the Camera
        StopFollowing();
        
        // - Return to original position
        targetPosition = originalPosition;
        targetRotation = originalRotation;
        Debug.Log("Returning To: " + originalPosition);

        // - Sets moving and return to true when transition is active
        moving = true;
        returningToPlayer = true;
    }

    void RestoreControlsInstant()
    {
        // - Restore Controlls for Player
        playerCamScript.enabled = true;
        moveCameraScript.enabled = true;
        playerControllerScript.enabled = true;

        returningToPlayer = false;
        hasStoredOriginal = false;
    }
    
    public void FocusOnTarget(Transform target)
    {
        // - Disables the PlayerCam,MoveCam and PlayerController scripts to fix fighting
        playerCamScript.enabled = false;
        moveCameraScript.enabled = false;
        playerControllerScript.enabled = false;

        if (!hasStoredOriginal)
        {
            // - Sets the new positions
            originalPosition = camHolder.position;
            originalRotation = camHolder.rotation;
            hasStoredOriginal = true;
            Debug.Log("Stored Original: " + originalPosition);
        }
        
        // - Sets the new positions
        targetPosition = target.position;
        targetRotation = target.rotation;

        moving = true;
    }
    
    public void StartFollowing(Transform target)
    {
        // - Set the follow target
        followTarget = target;
        following = true;

        // - Disables the PlayerCam,MoveCam and PlayerController scripts to fix fighting
        playerCamScript.enabled = false;
        moveCameraScript.enabled = false;
        playerControllerScript.enabled = false;
    }

    public void StopFollowing()
    {
        // - Removes follow target
        following = false;
        followTarget = null;
    }
}