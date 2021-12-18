using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoAnimation : MonoBehaviour
{
  

    public void RepositionParent()
    {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
    }

  
   
}



