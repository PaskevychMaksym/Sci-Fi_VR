using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MeteorSplitter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private Transform _shootPosition;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _breakTime = 2f;
    [SerializeField]
    private Color _targetColor = Color.green;

    private bool _isRayActive;
    private Breakable _currentBreakable;
    private float _currentBreakTime;
    private ParticleSystem.MinMaxGradient _originalColor;

    private void Awake()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.activated.AddListener(StartShoot);
        grabInteractable.deactivated.AddListener(StopShoot);
    }

    private void OnDestroy()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.activated.RemoveListener(StartShoot);
        grabInteractable.deactivated.RemoveListener(StopShoot);
    }

    private void Start()
    {
        var colorOverLifetime = _particleSystem.colorOverLifetime;
        _originalColor = colorOverLifetime.color;
    }

    private void Update()
    {
        if (_isRayActive)
        {
            RaycastCheck();
        }
    }

    private void StartShoot(ActivateEventArgs args)
    {
        _particleSystem.Play();
        AudioManager.instance.Play(Sound.AudioObject.Pistol);
        _isRayActive = true;
    }

    private void StopShoot(DeactivateEventArgs args)
    {
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        AudioManager.instance.Stop(Sound.AudioObject.Pistol);
        _isRayActive = false;
        ResetBreakable();
    }

    private void RaycastCheck()
    {
        if (Physics.Raycast(_shootPosition.position, _shootPosition.forward, out RaycastHit hit, _distance))
        {
            Breakable breakable = hit.collider.GetComponent<Breakable>();

            if (breakable != null)
            {
                if (_currentBreakable != breakable)
                {
                    ResetBreakable();
                    _currentBreakable = breakable;
                }

                _currentBreakTime += Time.deltaTime;
                SetParticleColor(_targetColor);

                if (!(_currentBreakTime >= _breakTime))
                {
                    return;
                }

                _currentBreakable.Break();
                ResetBreakable();
            } else
            {
                ResetBreakable();
            }
        } else
        {
            ResetBreakable();
        }
    }

    private void ResetBreakable()
    {
        _currentBreakable = null;
        _currentBreakTime = 0f;
        SetParticleColor(_originalColor);
    }

    private void SetParticleColor(Color color)
    {
        var colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(color);
    }

    private void SetParticleColor(ParticleSystem.MinMaxGradient gradient)
    {
        var colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.color = gradient;
    }
}
