using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadMainScene : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += StartGame;
    }

    private void StartGame(VideoPlayer vp)
    {
        SceneManager.LoadScene("MetroScene");
    }
}
