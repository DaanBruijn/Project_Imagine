using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// - EventRunner script which Manages the Evens in the Scene
// - Daniel Bruijn

public class EventRunner : MonoBehaviour
{
    // - Variable
    // - Public
    public static EventRunner Instance;
    
    [Header("NPC's")]
    public int activeNPCCount;

    // - Private
    [SerializeField] private List<NPCDialogue> allNPCs = new List<NPCDialogue>();
    [SerializeField] private List<NPCDialogue> activeNPCs = new List<NPCDialogue>();

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        StartCoroutine(FindNPCCO());
    }

    public void StartEvent(EventSequence sequence, EventContext context)
    {
        StartCoroutine(sequence.Play(context));
    }
    
    void SelectRandomActiveNPCs()
    {
        // - Clear previous selection if needed
        activeNPCs.Clear();
    
        List<NPCDialogue> shuffled = new List<NPCDialogue>(allNPCs);
        for (int i = 0; i < shuffled.Count; i++)
        {
            NPCDialogue temp = shuffled[i];
            int randomIndex = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
    
        // - Enables the selected amoun disables the rest
        for (int i = 0; i < shuffled.Count; i++)
        {
            BoxCollider box = shuffled[i].GetComponent<BoxCollider>();
        
            if (i < activeNPCCount)
            {
                shuffled[i].enabled = true;
                activeNPCs.Add(shuffled[i]);
                
                if (box != null)
                    box.enabled = true;
            }
            else
            {
                shuffled[i].enabled = false;
                
                if (box != null)
                    box.enabled = false;
            }
        }
    }

    IEnumerator FindNPCCO()
    {
        yield return new WaitForSeconds(1.5f);
        NPCDialogue[] npcs = FindObjectsOfType<NPCDialogue>();
        allNPCs.AddRange(npcs);
        activeNPCCount = (int)allNPCs.Count / 2;

        SelectRandomActiveNPCs();
    }
}