using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// - Stalker controller using NPCDialogue system
// - Daniel Bruijn (Refactored)
public class StalkerController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public StalkerLocation[] allLocations;

    [Header("Settings")] 
    public float detectionRadius = 15f;
    public float relocateCooldown = 5f;

    [Header("Pose System")]
    public Transform[] poses;

    [Header("Events")]
    public List<EventSequence> eventSequences;
    public List<NamedTarget> targets;

    // Private
    private StalkerLocation currentLocation;
    private StalkerLocation lastLocation;
    private float lastRelocateTime;

    private HashSet<EventSequence> triggeredEvents = new HashSet<EventSequence>();

    public Collider eventTrigger;

    private Coroutine currentSequence;

    void Start()
    {
        // Disable trigger initially
        if (eventTrigger != null)
            eventTrigger.enabled = false;

        StartCoroutine(LocateAllPositions());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > detectionRadius && Time.time > lastRelocateTime + relocateCooldown)
        {
            Relocate();
            lastRelocateTime = Time.time;

            if (eventTrigger != null)
                eventTrigger.enabled = true;
        }
    }

    void Relocate()
    {
        float teleportRadius = 40f;
        float minDistance = 10f;

        List<StalkerLocation> validLocations = allLocations
            .Where(loc =>
                loc != currentLocation &&
                loc != lastLocation &&
                Vector3.Distance(loc.transform.position, player.position) <= teleportRadius &&
                Vector3.Distance(loc.transform.position, player.position) >= minDistance
            ).ToList();

        if (validLocations.Count == 0)
        {
            validLocations = allLocations
                .Where(loc => loc != currentLocation && loc != lastLocation)
                .ToList();

            if (validLocations.Count == 0)
                validLocations = allLocations.ToList();
        }

        StalkerLocation closest = validLocations
            .OrderBy(loc => Vector3.Distance(loc.transform.position, player.position))
            .First();

        MoveToLocation(closest);
    }

    void MoveToLocation(StalkerLocation newLocation)
    {
        lastLocation = currentLocation;
        currentLocation = newLocation;
        transform.position = newLocation.transform.position;
        ApplyRandomPose();
    }

    void ApplyRandomPose()
    {
        if (poses.Length == 0) return;
        int index = Random.Range(0, poses.Length);
        Transform pose = poses[index];
        transform.rotation = pose.rotation;
    }

    IEnumerator LocateAllPositions()
    {
        yield return new WaitForSeconds(1.5f);
        allLocations = FindObjectsOfType<StalkerLocation>();
        Debug.Log("Found " + allLocations.Length + " locations.");
    }

    // Called by trigger
    public void StartConversation()
    {
        EventSequence nextEvent = eventSequences
            .FirstOrDefault(ev => !triggeredEvents.Contains(ev));

        if (nextEvent == null) return;

        EventContext context = new EventContext
        {
            player = GameObject.FindWithTag("Player"),
            npc = transform
        };

        foreach (var t in targets)
        {
            context.targets[t.key] = t.target;
        }

        EventRunner.Instance.StartEvent(nextEvent, context);
        triggeredEvents.Add(nextEvent);

        // Disable trigger after playing all events
        if (triggeredEvents.Count >= eventSequences.Count && eventTrigger != null)
        {
            eventTrigger.enabled = false;
        }
    }
}