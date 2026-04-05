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

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        stalkerController.StartConversation();

        if (triggerCollider != null)
            triggerCollider.enabled = false; // temporarily disable until next teleport
    }
}