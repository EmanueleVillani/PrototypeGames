using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlyScript : MonoBehaviour
{
    [SerializeField]
    private Transform ballAttackStartPoint;

    [SerializeField]
    private GameObject ball;

    

    

  
    public void AttackFly()
    {
        
        Instantiate(ball, ballAttackStartPoint.position, Quaternion.identity);

        
    }

  
   



}
