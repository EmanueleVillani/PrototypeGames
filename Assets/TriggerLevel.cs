using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            PlayerManager.Instance.level1done = true;
            GameManager.Instance.LoadSecondLevel();
        }
    }
}
