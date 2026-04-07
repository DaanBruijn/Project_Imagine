using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class endgameafterCutscene : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += endgame;
    }

    private void endgame(VideoPlayer vp)
    {
        Application.Quit();
    }
}
