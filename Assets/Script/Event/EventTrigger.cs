using UnityEngine;

// - Script for handling the starting of the Events
// - Daniel Bruijn

public class EventTrigger : MonoBehaviour
{
    // - Variables
    public NPCDialogue npc;

    void Start()
    {
        // - Set references
        npc = GetComponent<NPCDialogue>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        npc.StartConversation();
    }
}