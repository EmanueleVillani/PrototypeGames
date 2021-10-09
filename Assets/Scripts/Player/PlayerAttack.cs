using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamege; // creo una variabile danno
    private int enemyLayer;
    
    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == enemyLayer)
        {
            Debug.Log("Colpisco il nemico");
        }
    }
}
