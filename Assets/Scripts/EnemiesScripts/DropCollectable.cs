using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectable : MonoBehaviour
{

    [SerializeField]
    private GameObject[] collectables;

    public void CheckToSpawnCollectable()
    {
        GameObject where = GameObject.Find("PROPS");
        if(Random.Range(0, collectables.Length+1) < collectables.Length)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 0f, transform.position.z);
            GameObject collectable = Instantiate(collectables[Random.Range(0, collectables.Length)], spawnPos, Quaternion.identity, where.transform);
            if (collectable.gameObject.CompareTag("Battery"))
                collectable.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
}
