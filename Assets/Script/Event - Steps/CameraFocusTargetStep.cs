using UnityEngine;
using System.Collections;

// - Step for CameraFocusTarget
// - Used for following a target in a "cutscene"
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Camera Focus Target")]
public class CameraFocusTargetStep : EventStep
{
    // - Variables
    public string targetKey;
    public float waitTime = 1f;

    public override IEnumerator Execute(EventContext context)
    {
        if (context.targets.TryGetValue(targetKey, out Transform target))
        {
            DialogueCameraController.Instance.FocusOnTarget(target);
        }
        else
        {
            // - Check if Target Key is Empty
            Debug.LogWarning($"!!Target key '{targetKey}' not found in context!! - Check Insector");
        }

        yield return new WaitForSeconds(waitTime);
    }
}