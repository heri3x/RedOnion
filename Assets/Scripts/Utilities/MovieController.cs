using UnityEngine;
using UnityEngine.Video;

public class MovieController : MonoBehaviour
{
    public VideoPlayer player;
    private bool m_isPaused = false;


    public void TogglePlay()
    {
        if (!player.isPlaying && !m_isPaused)
        {
            player.Play();
        }
        else
        {
            player.Stop();
        }
        m_isPaused = false;
    }

    //ムービー終了時コールバック
    private void OnMovieEnded(UnityEngine.Video.VideoPlayer player)
    {
        //最終フレームで一時停止
        player.frame = (long)player.frameCount - 1;
        player.Pause();
        m_isPaused = true;
    }

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        player.loopPointReached += OnMovieEnded;
    }

    private void OnDestroy()
    {
        player.loopPointReached -= OnMovieEnded;
    }
}
