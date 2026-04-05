using UnityEngine;

public class TicketMachine : BaseObjectScript
{
    public bool workingMachine;
    public static bool gotTicket;

    public override void ActivateObject()
    {
        if (workingMachine)
        {
            gotTicket = true;
        }
    }
}
