using UnityEngine;

// - Works with StalkerController
// - Trigger for all the stalker events
// - Daniel Bruijn

public class StalkerTrigger : MonoBehaviour
{
    // - Variables
    private StalkerController stalkerController;
    private Collider triggerCollider;

    void Start()
    {
        stalkerController = GetComponentInParent<StalkerController>();
        triggerCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        stalkerController.StartConversation();

        if (triggerCollider != null)
            triggerCollider.enabled = false;
    }
}