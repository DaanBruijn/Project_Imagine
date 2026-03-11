using UnityEngine;

// - Script for the DialogueTrigger
// - If the player walks through the trigger box it starts the convo
// - Daniel Bruijn

public class NPCDialogueTrigger : MonoBehaviour
{
    // - Variables
    NPCDialogue npc;
    

    void Awake()
    {
        npc = GetComponent<NPCDialogue>();
    }

    void OnTriggerEnter(Collider other)
    {
        // - Checks if the Player hits the box otherwise return
        if (!other.CompareTag("Player"))
            return;

        // - Checks if the NPC is a speaker
        if (!npc.isSpeaker)
            return;
        
        npc.StartConversation();
    }
}