using UnityEngine;
using System.Collections;

// - Script for a Camera Follow Step
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Camera Follow")]
public class CameraFollowStep : EventStep
{
    public string targetKey;
    public float duration;

    public override IEnumerator Execute(EventContext context)
    {
        if (!context.targets.TryGetValue(targetKey, out Transform target))
        {
            // - Check if Target Key is Empty
            Debug.LogWarning($"!!Target key '{targetKey}' not found in context!! - Check Insector");
            yield break;
        }

        DialogueCameraController.Instance.StartFollowing(target);

        yield return new WaitForSeconds(duration);

        DialogueCameraController.Instance.StopFollowing();
    }
}