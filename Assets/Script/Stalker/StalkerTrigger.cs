using UnityEngine;

// Works with StalkerController
public class StalkerTrigger : MonoBehaviour
{
    private StalkerController stalkerController;
    private Collider triggerCollider;

    void Start()
    {
        stalkerController = GetComponentInParent<StalkerController>();
        triggerCollider = GetComponent<Collider>();
    }

    // Called by StalkerController to reset trigger
    public void EnableTrigger()
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;

            // If player is already inside, immediately start event
            Collider playerCol = GameObject.FindWithTag("Player")?.GetComponent<Collider>();
            if (playerCol != null && triggerCollider.bounds.Intersects(playerCol.bounds))
            {
                stalkerController.StartConversation();
                triggerCollider.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        stalkerController.StartConversation();

        if (triggerCollider != null)
            triggerCollider.enabled = false; // temporarily disable until next teleport
    }
}