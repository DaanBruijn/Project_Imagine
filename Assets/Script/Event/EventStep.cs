using UnityEngine;
using System.Collections;

// - ScriptableObject for the Event Steps
// - Daniel Bruijn

public abstract class EventStep : ScriptableObject
{
    // - Variables
    public abstract IEnumerator Execute(EventContext context);
}