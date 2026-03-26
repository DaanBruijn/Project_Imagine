using UnityEngine;
using System.Collections.Generic;

// - Script for the NPC Dialogue
// - Lets the Player Speak with the NPC and sets the Lines for the NPC
// - Daniel Bruijn

public class NPCDialogue : MonoBehaviour
{
    // - Varibales
    public EventSequence eventSequence;
    
    [Header("Optional: Targets")]
    public List<NamedTarget> targets;
    
    // - Private
    private bool hasTriggered = false;
    
    public void StartConversation()
    {
        // - Check if the NPC has already spoken
        if (hasTriggered) return;
        if (eventSequence == null) return;

        // - Set Event references
        EventContext context = new EventContext
        {
            player = GameObject.FindWithTag("Player"),
            npc = transform
        };
        
        // - Inject targets into context - If Selected
        foreach (var t in targets)
        {
            context.targets[t.key] = t.target;
        }

        EventRunner.Instance.StartEvent(eventSequence, context);
        hasTriggered = true;
    }
}

// - Small class for the Targets for CameraFocus
[System.Serializable]
public class NamedTarget
{
    public string key;
    public Transform target;
}