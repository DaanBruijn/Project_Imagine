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
    public Image fadeImage;
    public float fadeDuration = 1f;
    
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneName)
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
