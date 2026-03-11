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

    // - Private
    Vector3 originalPosition;
    Quaternion originalRotation;

    Vector3 targetPosition;
    Quaternion targetRotation;

    bool moving = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
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
            camHolder.position = targetPosition;
            camHolder.rotation = targetRotation;
            moving = false;
        }
    }

    public void FocusOnNPC(Transform npc)
    {
        // - Disables the PlayerCam,MoveCam and PlayerController scripts to fix fighting
        playerCamScript.enabled = false;
        moveCameraScript.enabled = false;
        playerControllerScript.enabled = false;

        // - Sets the new positions
        originalPosition = camHolder.position;
        originalRotation = camHolder.rotation;

        // - Finds the Anchor Point for Camera Position
        Transform anchor = npc.Find("DialogueCameraAnchor_Obj");
        if (anchor == null)
        {
            Debug.LogError("CameraAnchor missing on NPC!");
            return;
        }
        targetPosition = anchor.position;
        targetRotation = anchor.rotation;

        // - Sets moving to true when transition is active
        moving = true;
    }

    public void ReturnCamera()
    {
        // - Return to original position
        targetPosition = originalPosition;
        targetRotation = originalRotation;

        // - Sets moving to true when transition is active
        moving = true;
        
        StartCoroutine(RestoreControls());
    }

    IEnumerator RestoreControls()
    {
        yield return new WaitForSeconds(0.4f);

        // - Sets scripts back actie for PlayerControlls
        playerCamScript.enabled = true;
        moveCameraScript.enabled = true;
        playerControllerScript.enabled = true;
    }
}