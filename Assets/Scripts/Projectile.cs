using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed=2f;
    public float damage = 1f;
    public float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }  
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position+transform.up*0.2f);
    }
}
