using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// - Ticket Manager for selecting the "right" machine
// - Daniel Bruijn

public class TicketMachineManager : MonoBehaviour
{
    // - Variables
    public static TicketMachineManager Instance;
    public List<TicketMachine> allMachines = new List<TicketMachine>();
    public List<TicketMachine> usedMachines = new List<TicketMachine>();

    // - Private
    private bool workingMachineChosen = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(FindMachineCO());
    }

    public void RegisterUse(TicketMachine machine)
    {
        // - Avoid duplicationns
        if (usedMachines.Contains(machine))
            return;

        usedMachines.Add(machine);

        Debug.Log("Used machines: " + usedMachines.Count);

        if (!workingMachineChosen && usedMachines.Count >= 3)
        {
            ChooseWorkingMachine();
        }
    }

    private void ChooseWorkingMachine()
    {
        workingMachineChosen = true;
        
        // - List of unsued machine
        List<TicketMachine> availableMachines = new List<TicketMachine>();
        foreach (var machine in allMachines)
        {
            if (!machine.HasBeenUsed())
            {
                availableMachines.Add(machine);
            }
        }

        if (availableMachines.Count == 0)
        {
            Debug.LogWarning("No machines left to choose from!");
            return;
        }

        TicketMachine chosen = availableMachines[Random.Range(0, availableMachines.Count)];
        chosen.isWorkingMachine = true;
        Debug.Log("Working machine selected: " + chosen.name);
    }

    IEnumerator FindMachineCO()
    {
        yield return new WaitForSeconds(1.5f);
        allMachines = new List<TicketMachine>(FindObjectsByType<TicketMachine>(FindObjectsSortMode.None));
    }
}