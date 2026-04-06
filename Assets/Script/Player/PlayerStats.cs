using Unity.VisualScripting;
using UnityEngine;

// - Script for handling PlayerStat Data (Stress, Stamina, ETC.)
// - Daniel Bruijn

public class PlayerStats : MonoBehaviour
{
    // - Variables
    [Header("Stress")]
    public float stress = 0f;
    public float maxStress = 100f;

    void Start()
    {
        // - Makes sure that Stress is 0 at the start.
        stress = 0f;
    }

    void Update()
    {
        stress += Time.deltaTime * 0.001f;
        MaxStressReached();
    }
    
    public void AddStress(float amount)
    {
        stress += amount;
        stress = Mathf.Clamp(stress, 0, maxStress);

        Debug.Log("Stress: " + stress);

        // - Optional: update UI here - Visual Que, Vignette Color grading etc.
    }
    
    // - Optional: Use objects to reduce stress??
    public void ReduceStress(float amount)
    {
        stress -= amount;
        stress = Mathf.Clamp(stress, 0, maxStress);
    }

    public void MaxStressReached()
    {
        if (stress >= maxStress)
        {
            Camera.main.AddComponent<Rigidbody>();
            Camera.main.transform.parent = Camera.main.transform;

            Debug.Log("MaxStressReached - No event yet");
        }
    }


}