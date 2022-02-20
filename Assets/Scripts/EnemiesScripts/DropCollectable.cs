using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectable : MonoBehaviour
{

    [SerializeField]
    private GameObject[] collectables;

    public void CheckToSpawnCollectable()
    {

        if(Random.Range(0,2) > 0)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 0f, transform.position.z);
           GameObject collectable = Instantiate(collectables[Random.Range(0, collectables.Length)], spawnPos, Quaternion.identity);

            if (collectable.gameObject.CompareTag("Battery"))
                collectable.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }


    }
}
