using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TicketBoothManager : MonoBehaviour
{
    public UnityEvent ticketUsedEvent;
    public Ticket[] tickets;

    private AudioSource ticketBoothAudioSource;
    public void Awake()
    {
        tickets = FindObjectsByType<Ticket>(FindObjectsSortMode.None);
        tickets[Random.Range(1,tickets.Length)].isEndTicket = true;
        
        ticketBoothAudioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Ticket>(out Ticket ticketScript))
        {
            if (ticketScript.isEndTicket == true)
            {
                SceneManager.LoadScene("PlayTest_EndScene");
            }
            ticketBoothAudioSource.Play();
            
            ticketScript.UseTicket();
            ticketUsedEvent.Invoke();
        }
    }
}
