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

        // Скрыть модель руки только если пульт захвачен
        if ((XRBaseInteractor)args.interactorObject == _leftHandInteractor)
        {
            _leftHandModel.gameObject.SetActive(false); // Скрыть левую руку
        } else if ((XRBaseInteractor)args.interactorObject == _rightHandInteractor)
        {
            _rightHandModel.gameObject.SetActive(false); // Скрыть правую руку 
        }
    }

    private void OnRelease (SelectExitEventArgs args)
    {
        if (_currentGrabbedObject == null || (XRGrabInteractable)args.interactableObject != _currentGrabbedObject)
        {
            return;
        }

        // Вернуть модель руки, когда объект отпущен
        if ((XRBaseInteractor)args.interactorObject == _leftHandInteractor)
        {
            _leftHandModel.gameObject.SetActive(true); // Показать левую руку
        } else if ((XRBaseInteractor)args.interactorObject == _rightHandInteractor)
        {
            _rightHandModel.gameObject.SetActive(true); // Показать правую руку
        }

        _currentGrabbedObject = null;
    }
}