using UnityEngine;

public class Ticket : BaseObjectScript
{
    public bool isEndTicket = false;

    public override void ActivateObject()
    {
        transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 1) + (Camera.main.transform.right * 1);
        transform.parent = Camera.main.transform;
    }
    public void UseTicket()
    {

        Destroy(this.gameObject);
    }
}
