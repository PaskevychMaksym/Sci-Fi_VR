using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
   [SerializeField]
   private List<GameObject> _breakablePieces;

   private void Start()
   {
      foreach (var piece in _breakablePieces)
      {
         piece.SetActive(false);
      }
   }

   public void Break()
   {
      foreach (var piece in _breakablePieces)
      {
         piece.SetActive(true);
         piece.transform.parent = null;
      }
      
      gameObject.SetActive(false);
   }
}
