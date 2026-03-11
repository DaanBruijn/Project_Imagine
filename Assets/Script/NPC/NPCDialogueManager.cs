using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// - NPC Managers
// - Sets the amount of NPC's in the Scene that can speak wit the player
// - Daniel Bruijn

public class NPCDialogueManager : MonoBehaviour
{
    // - Variables
    [Header("Amount of Speakers")]
    public int speakersPerScene;
    
    void Start()
    {
        AssignSpeakers();
    }

    // - Setup for the "Speakers" 
    void AssignSpeakers()
    {
        // - Finds all NPCs
        NPCDialogue[] allNPCs = FindObjectsOfType<NPCDialogue>();

        // - Sets NPCs in a random order
        List<NPCDialogue> shuffled =
            allNPCs.OrderBy(x => Random.value).ToList();

        // - The first are set as Speakers (equal to SpeakersPerScene)/ Enables all Triggers for the speakers
        for (int i = 0; i < speakersPerScene && i < shuffled.Count; i++)
        {
            shuffled[i].isSpeaker = true;

            NPCDialogueTrigger trigger =
                shuffled[i].GetComponent<NPCDialogueTrigger>();

            if (trigger != null)
                trigger.enabled = true;
        }
    }
}