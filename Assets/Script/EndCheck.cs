using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCheck : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colliding");
        Debug.Log(TicketMachine.gotTicket);
        if (TicketMachine.gotTicket)
        {
            Debug.Log("startending");
            SceneManager.LoadScene("OutroCutscene");
        }
    }
}
