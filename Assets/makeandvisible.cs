using UnityEngine;
using UnityEngine.UI;

public class makeandvisible : MonoBehaviour
{
    private RawImage handimage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handimage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TicketMachine.gotTicket)
        {
            handimage.enabled = true;
        }
    }
}
