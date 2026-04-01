using UnityEngine;
using System.Collections;

// - Step for locking the player step
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Lock Player")]
public class LockPlayerStep : EventStep
{
    // - Variables
    public bool locked;

    public override IEnumerator Execute(EventContext context)
    {
        context.player.GetComponent<PlayerController>().enabled = !locked;
        yield break;
    }
}