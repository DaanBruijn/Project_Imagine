using UnityEngine;
using System.Collections;

// - EventRunner script which Manages the Evens in the Scene
// - Daniel Bruijn

public class EventRunner : MonoBehaviour
{
    // - Variable
    public static EventRunner Instance;

    void Awake()
    {
        // - Makes an Instance
        Instance = this;
    }

    public void StartEvent(EventSequence sequence, EventContext context)
    {
        StartCoroutine(sequence.Play(context));
    }
}