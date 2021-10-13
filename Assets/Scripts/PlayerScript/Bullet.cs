using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speedBullet=5;

    private void Update()
    {
       
        transform.Translate(speedBullet * Time.deltaTime * Vector3.right);
        
    }






}
