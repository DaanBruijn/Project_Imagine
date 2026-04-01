using UnityEngine;
using System.Collections;

// - Script for a AudioPlayStep
// - Daniel Bruijn

[CreateAssetMenu(menuName = "Events/Steps/Audio Play")]
public class AudioPlayStep : EventStep
{
    // - Variables
    // - Public
    public AudioClip clip;
    public float volume = 1f;
    public bool waitUntilFinished = true;

    public override IEnumerator Execute(EventContext context)
    {
        Debug.Log("AudioPlayStep Execute called");
        if (clip == null)
        {
            Debug.LogWarning("!! - No AudioClip assigned in AudioPlayStep - !!");
            yield break;
        }

        //  - Try to get an AudioSource from context
        if (!context.targets.TryGetValue("audioSource", out Transform sourceTransform))
        {
            Debug.LogWarning("!! - No AudioSource found in key 'audioSource' - !!");
            yield break;
        }

        AudioSource audioSource = sourceTransform.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("!! - Transform does not have an AudioSource - !!");
            yield break;
        }

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        if (waitUntilFinished)
        {
            yield return new WaitForSeconds(clip.length);
        }
    }
}