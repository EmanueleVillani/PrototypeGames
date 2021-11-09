using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlyScript : MonoBehaviour
{
    [SerializeField]
    private Transform ballAttackStartPoint;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private Transform insectParentPosition;

    private float speedEnemy;

    private void Start()
    {
        speedEnemy = GetComponentInParent<FlyingEnemy>().speed;
    }


    public void AttackFly()
    {






        Instantiate(ball, ballAttackStartPoint.position, Quaternion.identity);

        

        
    }

  
   



}
