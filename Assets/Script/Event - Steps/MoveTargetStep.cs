using UnityEngine;
using System.Collections;

// - Script for moving Objects for the Target Camera
// - Used for making it see like something is moving in the scene
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Move Target")]
public class MoveTargetStep : EventStep
{
    public string targetKey;
    public string destinationKey;
    public float duration;

    public override IEnumerator Execute(EventContext context)
    {
        if (!context.targets.TryGetValue(targetKey, out Transform target))
        {
            // - Check if Target Key is Empty
            Debug.LogWarning($"!!Target key '{targetKey}' not found in context!! - Check Insector");
            yield break;
        }

        if (!context.targets.TryGetValue(destinationKey, out Transform destination))
        {
            // - Check if Destination Key is Empty
            Debug.LogWarning($"!!Target key '{destinationKey}' not found in context!! - Check Insector");
            yield break;
        }

        Vector3 startPos = target.position;
        Quaternion startRot = target.rotation;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            target.position = Vector3.Lerp(startPos, destination.position, t);
            target.rotation = Quaternion.Lerp(startRot, destination.rotation, t);

            time += Time.deltaTime;
            yield return null;
        }

        target.position = destination.position;
        target.rotation = destination.rotation;
    }
}