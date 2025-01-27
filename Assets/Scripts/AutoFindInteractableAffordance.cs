using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.State;

public class AutoFindInteractableAffordance : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<XRInteractableAffordanceStateProvider>().interactableSource = GetComponentInParent<XRBaseInteractable>();
    }
}
