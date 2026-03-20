using UnityEngine;
using System.Collections;

// - Step for the Stress System
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Add Stress")]
public class StressStep : EventStep
{
    // - Public
    public float amount;

    public override IEnumerator Execute(EventContext context)
    {
        context.player.GetComponent<PlayerStats>().AddStress(amount);

        yield break;
    }
}