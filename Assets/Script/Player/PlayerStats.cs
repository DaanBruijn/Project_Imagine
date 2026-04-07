using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// - Script for handling PlayerStat Data (Stress, Stamina, ETC.)
// - Daniel Bruijn

public class PlayerStats : MonoBehaviour
{
    // - Variables
    [Header("Stress")]
    public float stress = 0f;
    public float maxStress = 100f;

    [Header("UI")] 
    public Image fadeImage;
    public float fadeDuration = 6f;
    
    [Header("Player")]
    public PlayerController playerController;

    void Start()
    {
        // - Makes sure that Stress is 0 at the start.
        stress = 0f;
        // - Make sure PlayerController is Active
        playerController.enabled = true;
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
    }
    
    public void ReduceStress(float amount)
    {
        stress -= amount;
        stress = Mathf.Clamp(stress, 0, maxStress);
    }

    public void MaxStressReached()
    {
        if (stress >= maxStress)
        {
            Rigidbody rb = Camera.main.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            Camera.main.AddComponent<BoxCollider>();
            
            StartCoroutine(FadeAndLoadCO());
            playerController.enabled = false;
        }
    }
    
    IEnumerator FadeAndLoadCO()
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene("MainMenuScene");
    }
}