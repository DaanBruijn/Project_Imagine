using UnityEngine;
using System.Collections;

// - Step for the Camera Retrun for the Event System
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Camera Return")]
public class CameraReturnStep : EventStep
{
    public override IEnumerator Execute(EventContext context)
    {
        DialogueCameraController.Instance.ReturnCamera();
        yield return new WaitForSeconds(1f);
    }
}