using UnityEngine;

public class DoorController : BaseInteractable
{
  private const string IS_OPEN = "IsOpen";
  
  private Animator _animator;
  private bool _isOpen;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public override void ToggleState()
  {
    _isOpen = !_isOpen;
    _animator.SetBool(IS_OPEN, _isOpen);
  }
}