using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// KillAC专用帧动画脚本：4帧单播，播放后自动失活
/// </summary>
public class UIFrameAnimation : MonoBehaviour
{
    [Header("动画配置")]
    public Sprite[] animationFrames;
    public float frameRate = 10f;
    public bool isLoop = false;
    public bool autoPlay = false;

    private Image _targetImage;
    private float _frameInterval;
    private float _timer;
    private int _currentFrameIndex;
    private bool _isPlaying;

    void Awake()
    {
        _targetImage = GetComponent<Image>();
        if (_targetImage == null)
        {
            Debug.LogError("UIFrameAnimation：请挂载到带Image的UI对象！");
            enabled = false;
            return;
        }

        _frameInterval = frameRate <= 0 ? 0.1f : 1f / frameRate;
        _currentFrameIndex = 0;
        _isPlaying = false;
        _timer = 0;

        if (animationFrames != null && animationFrames.Length > 0)
        {
            _targetImage.sprite = animationFrames[0];
        }
    }

    void Start()
    {
        if (autoPlay) Play();
    }

    void Update()
    {
        if (!_isPlaying || animationFrames == null || animationFrames.Length <= 1) return;

        _timer += Time.unscaledDeltaTime;
        if (_timer >= _frameInterval)
        {
            _timer -= _frameInterval;
            NextFrame();
        }
    }

    public void Play()
    {
        if (animationFrames == null || animationFrames.Length == 0)
        {
            Debug.LogWarning("UIFrameAnimation：帧序列为空！");
            return;
        }

        gameObject.SetActive(true);
        _isPlaying = true;
        _timer = 0;
        _currentFrameIndex = 0;
        _targetImage.sprite = animationFrames[_currentFrameIndex];
        Debug.Log("UIFrameAnimation：KillAC动画开始播放");
    }

    private void NextFrame()
    {
        _currentFrameIndex++;
        if (_currentFrameIndex >= animationFrames.Length)
        {
            if (isLoop)
            {
                _currentFrameIndex = 0;
            }
            else
            {
                _currentFrameIndex = animationFrames.Length - 1;
                _isPlaying = false;
                gameObject.SetActive(false);
                Debug.Log("UIFrameAnimation：KillAC动画播放完成，自动失活");
                return;
            }
        }

        if (_currentFrameIndex < animationFrames.Length)
        {
            _targetImage.sprite = animationFrames[_currentFrameIndex];
        }
    }

    public void Pause() { _isPlaying = false; }
    public void Stop()
    {
        _isPlaying = false;
        _currentFrameIndex = 0;
        _timer = 0;
        gameObject.SetActive(false);
    }
}