using UnityEngine;

// - Script for the ticket machines
// - Daniel Bruijn

public class TicketMachine : MonoBehaviour, InterInteractable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private AudioClip _workingSound;
    [SerializeField] private GameObject _ticket;
    public bool isWorkingMachine = false;
    private bool hasBeenUsed = false;
    public static bool gotTicket;

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
            gotTicket = true;
            Debug.Log(TicketMachine.gotTicket);
            _audioSource.clip = _workingSound;
            _audioSource.Play();
            Debug.Log("THIS is the working machine!");
        }
        else
        {
            _audioSource.clip = _sounds[Random.Range(1, _sounds.Length - 1)];
            _audioSource.Play();
            Debug.Log("This machine is broken...");
        }
    }

    public string GetInteractionText()
    {
        return hasBeenUsed ? "Already Used" : "Use Ticket Machine (E)";
    }

    public bool HasBeenUsed()
    {
        return hasBeenUsed;
    }
}