using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// - ScriptableObject for the actual Event Sequence
// - Makes a list for all the EventSteps in the EventSequence
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Event Sequence")]
public class EventSequence : ScriptableObject
{
    // - Variables
    public List<EventStep> steps;

    public IEnumerator Play(EventContext context)
    {
        foreach (var step in steps)
        {
            yield return step.Execute(context);
        }
    }
}