using UnityEngine;
using System.Collections.Generic;

// - Script for the NPC Dialogue
// - Lets the Player Speak with the NPC and sets the Lines for the NPC
// - Daniel Bruijn

public class NPCDialogue : MonoBehaviour
{
    // - Varibales
    [Header("Dialogue")]
    public List<string> dialogueLines = new List<string>();

    [Header("Is Speaker")]
    public bool isSpeaker = false;
    
    private bool hasSpokenToPlayer = false;
    
    public void StartConversation()
    {
        if (!hasSpokenToPlayer)
        {
            // - Returns if the NPC is not a Speaker that run || Returns if the character has no lines || Returns if the player has already spoken to the npc
            if (!isSpeaker) return;
            if (dialogueLines.Count == 0) return;
            if (hasSpokenToPlayer) return;

            // - Starts Dialogue
            DialogueUI.Instance.StartDialogue(dialogueLines);

            DialogueCameraController.Instance.FocusOnNPC(transform);
            hasSpokenToPlayer = true;
        }
        
    }
}