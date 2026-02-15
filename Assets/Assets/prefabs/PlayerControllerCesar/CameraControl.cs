using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
    public bool lerpOrNot;

    [SerializeField] private float lookSensitivity;
    [SerializeField] private float upDownDifference;
    [SerializeField] private float lookLimit;
    [SerializeField] private float cameraMoveSmoothing;

    private float currentRotation = 0;
    InputAction look;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        look = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LerpPlayerCamera();
        RotatePlayerAndCamera();
    }

    void RotatePlayerAndCamera()
    {
        Vector2 currentLookDirection = look.ReadValue<Vector2>();
        if (currentLookDirection.magnitude > 1)
        {

        }
        playerTransform.Rotate(new Vector3(0f,currentLookDirection.x * lookSensitivity,0f));


        currentRotation += -currentLookDirection.y * upDownDifference * lookSensitivity;
        currentRotation = Mathf.Clamp(currentRotation, -lookLimit, lookLimit);
        cameraTransform.rotation = Quaternion.Lerp(Quaternion.Euler(cameraTransform.eulerAngles),Quaternion.Euler(currentRotation, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z),cameraMoveSmoothing);
        // cameraTransform.Rotate(new Vector3(-upDownRotation * lookSensitivity * upDownDifference, 0f, 0f));

        cameraTransform.rotation = Quaternion.Lerp(Quaternion.Euler(cameraTransform.eulerAngles), Quaternion.Euler(cameraTransform.eulerAngles.x, playerTransform.eulerAngles.y, cameraTransform.eulerAngles.z), cameraMoveSmoothing);
    }
    void LerpPlayerCamera()
    {
        if (lerpOrNot)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTransform.position, cameraMoveSmoothing);
        }
        else
        {
            cameraTransform.position = playerTransform.position;
        }
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
