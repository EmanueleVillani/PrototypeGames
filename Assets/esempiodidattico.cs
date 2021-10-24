using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esempiodidattico : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {//vector4 * vector3                                                                    //( da A a (B+(1,0,0)) = (p1.x+1, p1.y+0,p1.z+0)
        Gizmos.DrawLine(transform.position,transform.position+(transform.forward*0.5f));   //( da p1 a (p1+(1,0,0)) = (p1.x+1, p1.y+0,p1.z+0)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,2f);
    }
    //a b 
    //a->b
    //b-a=c
    //A(0.33,0,4.37)
    //B(1.33,0,4.37)
    //(1.33,0,4.37)-(0.33,0,4.37)=(1,0,0)
    //(1,0,0).magnitudine=1
    //(0.5,0,0).magnitudine=0.5 
}
