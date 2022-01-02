using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoAnimation : MonoBehaviour
{
    private Animator anim;
    private EnemyHealth health;
    private float animSpeed=15f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (health.isDead)
        {
            transform.Translate(Vector3.down * Time.deltaTime * animSpeed);

            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        }
        
    }

}



