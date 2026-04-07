using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // - Variables
    public Image fadeImage;
    public float fadeDuration = 1;

    void Start()
    {
        StartCoroutine(FadeInCO());
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
}
