using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableController : MonoBehaviour
{
  [SerializeField]
  private BaseInteractable _interactableObject;
  [SerializeField]
  private XRSimpleInteractable _interactor;

  private void OnEnable()
  {
    _interactor.selectEntered.AddListener(OnButtonPressed);
  }

  private void OnDisable()
  {
    _interactor.selectEntered.RemoveListener(OnButtonPressed);
  }

  private void OnButtonPressed(SelectEnterEventArgs args)
  {
    _interactableObject?.ToggleState();
  }
}