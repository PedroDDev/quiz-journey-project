using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using System;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    private PlayerMovement _player;
    private MapPoint _mapPoint;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject maxmizeScreenButton;
    [SerializeField] private GameObject minimizeScreenButton;

    [SerializeField] private GameObject videoPanel;

    [SerializeField] private RectTransform videoArea;

    private bool _isDone;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();

        videoArea.localScale = new Vector3(0.75f, 0.75f, 1f);
        videoArea.anchoredPosition = new Vector2(-1812.085f, -16);
    }

    void Update()
    {
        if (!IsPrepared) return;

        ChangeSlider();
        ChangeTimer();

        if (_isDone)
        {
            _mapPoint = GameObject.Find($"Map_Point_{_player.currentMapPointIndex + 1}").GetComponent<MapPoint>();
            _mapPoint.isLocked = false;
        }
    }

    void OnEnable()
    {
        _videoPlayer.errorReceived += ErrorReceived;
        _videoPlayer.frameReady += FrameReady;
        _videoPlayer.loopPointReached += LoopPointReached;
        _videoPlayer.prepareCompleted += PrepareCompleted;
        _videoPlayer.seekCompleted += SeekCompleted;
        _videoPlayer.started += Started;
    }

    void OnDisable()
    {
        _videoPlayer.errorReceived -= ErrorReceived;
        _videoPlayer.frameReady -= FrameReady;
        _videoPlayer.loopPointReached -= LoopPointReached;
        _videoPlayer.prepareCompleted -= PrepareCompleted;
        _videoPlayer.seekCompleted -= SeekCompleted;
        _videoPlayer.started -= Started;
    }

    public bool IsPlaying
    {
        get { return _videoPlayer.isPlaying; }
    }
    public bool IsLooping
    {
        get { return _videoPlayer.isLooping; }
    }
    public bool IsPrepared
    {
        get { return _videoPlayer.isPrepared; }
    }
    public bool IsDone
    {
        get { return _isDone; }
    }
    public double Time
    {
        get { return _videoPlayer.time; }
    }
    public ulong Duration
    {
        get { return (ulong)(_videoPlayer.frameCount / _videoPlayer.frameRate); }
    }
    public double NTime
    {
        get { return Time / Duration; }
    }

    void ErrorReceived(VideoPlayer v, string msg)
    {
        Debug.Log("Video player error: " + msg);
    }

    void FrameReady(VideoPlayer v, long frame)
    {
        //cpu tax is heavy
    }

    void LoopPointReached(VideoPlayer v)
    {
        Debug.Log("Video player loop point reached");
        restartButton.SetActive(true);
        playButton.SetActive(false);
        pauseButton.SetActive(false);
        _isDone = true;
    }
    void PrepareCompleted(VideoPlayer v)
    {
        Debug.Log("Video player finished preparing");
        _isDone = false;
    }
    void SeekCompleted(VideoPlayer v)
    {
        Debug.Log("Video player finished seeking");
        _isDone = false;
    }
    void Started(VideoPlayer v)
    {
        Debug.Log("Video player started");
    }

    public void LoadVideo(string name)
    {
        string temp = $"https://pedrohenriquediasbatista.github.io/quiz-journey-project-videos/{name}";
        if (_videoPlayer.url == temp) return;

        _videoPlayer.url = temp;
        _videoPlayer.Prepare();

        Debug.Log("can set direct audio volume: " + _videoPlayer.canSetDirectAudioVolume);
        Debug.Log("can set playback speed: " + _videoPlayer.canSetPlaybackSpeed);
        Debug.Log("can set skip on drop: " + _videoPlayer.canSetSkipOnDrop);
        Debug.Log("can set time: " + _videoPlayer.canSetTime);
        Debug.Log("can step: " + _videoPlayer.canStep);
    }
    public void PlayVideo()
    {
        if (!IsPrepared) return;

        playButton.SetActive(false);
        pauseButton.SetActive(true);

        _videoPlayer.Play();
    }
    public void PauseVideo()
    {
        if (!IsPlaying) return;

        pauseButton.SetActive(false);
        playButton.SetActive(true);

        _videoPlayer.Pause();
    }
    public void RestartVideo()
    {
        if (!IsPrepared) return;

        restartButton.SetActive(false);
        pauseButton.SetActive(true);

        Seek(0);
        PlayVideo();
    }
    public void LoopVideo(bool toggle)
    {
        if (!IsPrepared) return;

        _videoPlayer.isLooping = toggle;
    }
    public void Seek(float nTime)
    {
        if (!_videoPlayer.canSetTime) return;
        if (!IsPrepared) return;

        PauseVideo();
        nTime = Mathf.Clamp(nTime, 0, 1);
        _videoPlayer.time = nTime * Duration;
    }
    public void IncrementPlaybackSpeed()
    {
        if (!_videoPlayer.canSetPlaybackSpeed) return;

        _videoPlayer.playbackSpeed += 1;
        _videoPlayer.playbackSpeed = Mathf.Clamp(_videoPlayer.playbackSpeed, 0, 10);
    }
    public void DecrementPlaybackSpeed()
    {
        if (!_videoPlayer.canSetPlaybackSpeed) return;

        _videoPlayer.playbackSpeed -= 1;
        _videoPlayer.playbackSpeed = Mathf.Clamp(_videoPlayer.playbackSpeed, 0, 10);
    }

    void ChangeTimer()
    {
        TimeSpan duration = TimeSpan.FromSeconds(Duration);
        TimeSpan currentTime = TimeSpan.FromSeconds(Time);

        timerText.text = string.Format("{0,1:0}:{1,2:00}", currentTime.Minutes, currentTime.Seconds) + " / " + string.Format("{0,1:0}:{1,2:00}", duration.Minutes, duration.Seconds);
    }
    void ChangeSlider()
    {
        slider.value = (float)NTime;
    }

    public void ShowPanel()
    {
        videoPanel.SetActive(true);
        restartButton.SetActive(false);
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        _isDone = false;

        if (!IsPrepared) return;

        Seek(0);
        PauseVideo();
    }
    public void ClosePanel()
    {
        videoPanel.SetActive(false);
        _isDone = false;

        if (!IsPrepared) return;

        Seek(0);
        PauseVideo();
    }

    public void MaxmizeScreen()
    {
        maxmizeScreenButton.gameObject.SetActive(false);
        minimizeScreenButton.gameObject.SetActive(true);

        videoArea.localScale = new Vector3(1f, 1f, 1f);
        videoArea.anchoredPosition = new Vector2(0, 0);
    }

    public void MinimizeScreen()
    {
        minimizeScreenButton.gameObject.SetActive(false);
        maxmizeScreenButton.gameObject.SetActive(true);

        videoArea.localScale = new Vector3(0.75f, 0.75f, 1f);
        videoArea.anchoredPosition = new Vector2(0, -16);
    }
}
