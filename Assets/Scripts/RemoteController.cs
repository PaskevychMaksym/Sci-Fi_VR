using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RemoteController : XRGrabInteractable
{
  [SerializeField]
  private TVController _tvController;

  protected override void OnSelectEntered(SelectEnterEventArgs args)
  {
    base.OnSelectEntered(args);
    
    onActivate.AddListener(ActivateRemote);
  }

  protected override void OnSelectExited(SelectExitEventArgs args)
  {
    base.OnSelectExited(args);
    
    onActivate.RemoveListener(ActivateRemote);
  }
  
  private void ActivateRemote(XRBaseInteractor interactor)
  {
    _tvController.TogglePower();
  }
  
  private void OnTriggerPress(XRController controller)
  {
    InputDevice device = controller.inputDevice;
    
    if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed) && isTriggerPressed)
    {
      _tvController.TogglePower();
    }
    
    if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPrimaryButtonPressed) && isPrimaryButtonPressed)
    {
      _tvController.TogglePlayPause();
    }
    
    if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isSecondaryButtonPressed) && isSecondaryButtonPressed)
    {
      _tvController.NextClip();
    }
  }
}