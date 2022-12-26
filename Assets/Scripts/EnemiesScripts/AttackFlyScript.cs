using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlyScript : MonoBehaviour
{
    public Transform ballAttackStartPoint;

    [SerializeField]
    private GameObject ball;

   
    public void AttackFly()
    {
        GameObject atk=Instantiate(ball, ballAttackStartPoint.position, Quaternion.identity);
        atk.GetComponent<BallAttackScript>().Shoot(GetComponentInParent<FlyingEnemy>().attackDirection);
    }


}
