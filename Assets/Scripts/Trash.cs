using UnityEngine;

public class Trash : MonoBehaviour
{
  private void OnTriggerEnter (Collider other)
  {
    if (other.TryGetComponent(out TrashableItem trashableItem ))
    {
      trashableItem.gameObject.SetActive(false);
    }
  }
}
