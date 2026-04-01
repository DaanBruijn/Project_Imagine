using UnityEngine;
using System.Collections.Generic;

// - Script for the Events Context
// - Daniel Bruijn

public class EventContext
{
    // - Variables
    public GameObject player;
    public Transform npc;
    public Dictionary<string, Transform> targets = new Dictionary<string, Transform>();
}
