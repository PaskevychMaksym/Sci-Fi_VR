using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandVisibilityController : MonoBehaviour
{
    [SerializeField]
    private HandModel _rightHandModel;
    [SerializeField]
    private HandModel _leftHandModel;
    [SerializeField]
    private XRBaseInteractor _rightHandInteractor;
    [SerializeField]
    private XRBaseInteractor _leftHandInteractor;

    private XRGrabInteractable _currentGrabbedObject;

    private void Start()
    {
        _leftHandInteractor.selectEntered.AddListener(OnGrab);
        _leftHandInteractor.selectExited.AddListener(OnRelease);
        
        _rightHandInteractor.selectEntered.AddListener(OnGrab);
        _rightHandInteractor.selectExited.AddListener(OnRelease);
    }

    private void OnGrab (SelectEnterEventArgs args)
    {
        if (args.interactableObject is not XRGrabInteractable interactable)
        {
            return;
        }

        _currentGrabbedObject = interactable;

        if ((XRBaseInteractor)args.interactorObject == _leftHandInteractor)
        {
            _leftHandModel.gameObject.SetActive(false);
        }else if ((XRBaseInteractor)args.interactorObject == _rightHandInteractor)
        {
            _rightHandModel.gameObject.SetActive(false);
        }
    }

    private void OnRelease (SelectExitEventArgs args)
    {
        if ((XRGrabInteractable)args.interactableObject != _currentGrabbedObject)
        {
            return;
        }

        if ((XRBaseInteractor)args.interactorObject == _leftHandInteractor)
        {
            _leftHandModel.gameObject.SetActive(true);
        }else if ((XRBaseInteractor)args.interactorObject == _rightHandInteractor)
        {
            _rightHandModel.gameObject.SetActive(true);
        }

        _currentGrabbedObject = null;
    }
}
