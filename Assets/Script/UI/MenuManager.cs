using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// - MenuManager script for buttons
// - Daniel Bruijn & Cesar Waalders

public class MenuManager : MonoBehaviour
{
    // - Variables
    [Header("Audio")]
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource audioSource;
    
    [Header("Image")]
    public Image fadeImage = null;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeInCO());
    }
    
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        StartCoroutine(FadeAndLoadCO(sceneName));
    }

    IEnumerator FadeInCO()
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        color.a = 0;
        fadeImage.color = color;
    }

    IEnumerator FadeAndLoadCO(string sceneName)
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

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonSound()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
    }
}
