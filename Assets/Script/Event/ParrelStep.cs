using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// - A step event for running two steps at the same time - Use in example: Camera Follow / Move Target
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Parallel Step")]
public class ParallelStep : EventStep
{
    // - Variable
    public List<EventStep> steps;

    public override IEnumerator Execute(EventContext context)
    {
        List<Coroutine> running = new List<Coroutine>();

        foreach (var step in steps)
        {
            running.Add(EventRunner.Instance.StartCoroutine(step.Execute(context)));
        }

        // - Wait until steps  are done
        foreach (var step in steps)
        {
            yield return step.Execute(context);
        }
    }
}