using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    private const string IS_OPEN = "IsOpen";

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _screen;
    [SerializeField]
    private Material _videoMaterial;
    [SerializeField]
    private List<VideoClip> _videoClips;

    private bool _isOn;
    private int _currentClipIndex;
    private MeshRenderer _meshRenderer;
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _meshRenderer = _screen.GetComponent<MeshRenderer>();
        _videoPlayer = _screen.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += OnVideoEnded;

        if (_videoClips.Count > 0)
        {
            _videoPlayer.clip = _videoClips[0];
        }
    }

    private void OnEnable()
    {
        ApplyVideoMaterial();
    }

    private void OnDisable()
    {
        ApplyVideoMaterial();
    }

    private void OnDestroy()
    {
        _videoPlayer.loopPointReached -= OnVideoEnded;
    }

    public void TogglePower()
    {
        if (_isOn)
        {
            StopVideo();
            _isOn = false;
            _animator.SetBool(IS_OPEN, false);
        }
        else
        {
            _animator.SetBool(IS_OPEN, true);
            _isOn = true;
        }
    }

    public void OnAnimationFinished()
    {
        // Эта функция вызывается после завершения анимации
        if (_isOn)
        {
            PlayVideo();
        }
        else
        {
            _isOn = false;
            _videoPlayer.Stop();
        }
    }

    public void NextClip()
    {
        if (!_isOn)
        {
            return;
        }

        _currentClipIndex = (_currentClipIndex + 1) % _videoClips.Count;
        PlayVideo();
    }

    public void TogglePlayPause()
    {
        if (!_isOn)
        {
            return;
        }

        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }
        else
        {
            _videoPlayer.Play();
        }
    }

    private void PlayVideo()
    {
        if (_videoClips.Count > 0)
        {
            _videoMaterial.color = Color.white;
            _videoPlayer.clip = _videoClips[_currentClipIndex];
            _videoPlayer.Play();
        }
    }

    private void StopVideo()
    {
        _videoMaterial.color = Color.black;
        _videoPlayer.Stop();
    }

    private void ApplyVideoMaterial()
    {
        _meshRenderer.material = _videoMaterial;
    }

    private void OnVideoEnded(VideoPlayer source)
    {
        NextClip(); // Переход к следующему видео после окончания текущего
    }
}
