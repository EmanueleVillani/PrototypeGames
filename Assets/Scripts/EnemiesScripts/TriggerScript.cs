using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
       

    [SerializeField]
    private float distanceSpawn = 5f;



    private bool hasSpawned;  //this bool controls the spawn of the ghost


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSpawned)   
        {
            if (collision.CompareTag("Player"))
            {

                if (Random.Range(0, 2) == 1)
                    Instantiate(enemy, new Vector3(transform.position.x - distanceSpawn, transform.position.y, 0), Quaternion.identity);
                else
                    Instantiate(enemy, new Vector3(transform.position.x + distanceSpawn, transform.position.y, 0), Quaternion.identity);

                hasSpawned = true;
            }
        }
        
    }
}
