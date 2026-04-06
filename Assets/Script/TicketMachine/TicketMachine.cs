using UnityEngine;

// - Script for the ticket machines
// - Daniel Bruijn

public class TicketMachine : MonoBehaviour, InterInteractable
{
    public bool isWorkingMachine = false;
    private bool hasBeenUsed = false;

    public void Interact()
    {
        if (hasBeenUsed)
        {
            Debug.Log("Already used this machine.");
            return;
        }

        hasBeenUsed = true;

        TicketMachineManager.Instance.RegisterUse(this);

        if (isWorkingMachine)
        {
            Debug.Log("THIS is the working machine!");
        }
        else
        {
            Debug.Log("This machine is broken...");
        }
    }

    public string GetInteractionText()
    {
        return hasBeenUsed ? "" : "Use Ticket Machine (E)";
    }

    public bool HasBeenUsed()
    {
        return hasBeenUsed;
    }
}