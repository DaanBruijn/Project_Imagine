using UnityEngine;

public class Ticket : BaseObjectScript
{
    public bool isEndTicket = false;
    public static bool holdingTicket;
    public override void ActivateObject()
    {
        if (!holdingTicket)
        {
            holdingTicket = true;
            transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 1) + (Camera.main.transform.right * 1);
            transform.parent = Camera.main.transform;
        }
    }
    public void UseTicket()
    {
        holdingTicket = false;
        Destroy(this.gameObject);
    }
}
