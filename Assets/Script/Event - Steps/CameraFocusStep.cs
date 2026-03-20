using UnityEngine;
using System.Collections;

// - Step for the Camera Focus for the Event System
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Camera Focus")]
public class CameraFocusStep : EventStep
{
    public override IEnumerator Execute(EventContext context)
    {
        DialogueCameraController.Instance.FocusOnNPC(context.npc);
        yield return new WaitForSeconds(1f);
    }
}