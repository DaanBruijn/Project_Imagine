using UnityEngine;

public class InteractionTrigger : MonoBehaviour
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