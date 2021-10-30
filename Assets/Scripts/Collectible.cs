using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Item m_self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InventoryManager.Instance.ModifyInventory(m_self, true,1);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InventoryManager.Instance.ModifyInventory(m_self, true,1);
            Destroy(gameObject);
        }
    }
   
}
