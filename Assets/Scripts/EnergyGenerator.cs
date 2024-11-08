using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnergyGenerator : MonoBehaviour 
{
    private static readonly int color = Shader.PropertyToID("_EmissionColor");
    
    [SerializeField]
    private XRSocketInteractor _socketInteractor;
    [SerializeField]
    private List<Light> _lights;

    private void OnEnable()
    {
        _socketInteractor.selectEntered.AddListener(OnCubeInserted);
        _socketInteractor.selectExited.AddListener(OnCubeRemoved);
    }
    
    private void OnDisable()
    {
        _socketInteractor.selectEntered.RemoveListener(OnCubeInserted);
        _socketInteractor.selectExited.RemoveListener(OnCubeRemoved);
    }

    private void OnCubeInserted(SelectEnterEventArgs args)
    {
        GameObject insertedObject = args.interactableObject.transform.gameObject;
        Renderer renderer = insertedObject.GetComponent<Renderer>();

        if (renderer == null || !renderer.material.HasProperty(color))
        {
            return;
        }

        Color emissionColor = renderer.material.GetColor(color);
        Color normalizedColor = NormalizeColor(emissionColor);
        SetLightsColor(normalizedColor);
        SetLightsActive(true);
    }
    
    private void OnCubeRemoved(SelectExitEventArgs args)
    {
        SetLightsActive(false);
    }
    
    private void SetLightsColor(Color color)
    {
        foreach (Light light in _lights)
        {
            if (light != null)
            {
                light.color = color;
            }
        }
    }

    private void SetLightsActive(bool isActive)
    {
        foreach (Light light in _lights)
        {
            if (light != null)
            {
                light.enabled = isActive;
            }
        }
    }
    
    private Color NormalizeColor(Color color)
    {
        float maxComponent = Mathf.Max(color.r, color.g, color.b);
        if (maxComponent > 0)
        {
            color /= maxComponent;
        }
        return color;
    }
}
