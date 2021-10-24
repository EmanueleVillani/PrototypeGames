using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByAnimation : MonoBehaviour
{
      Animator anim;
    GatherInput gI;
    public GameObject target;
    public bool isRight = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gI = GetComponent<GatherInput>();
    }
    float x = 1f;
    // Update is called once per frame
    void Update()
    {
        if((isRight&& target.transform.position.x < transform.position.x) || (!isRight && target.transform.position.x > transform.position.x))
        {
            isRight ^= true;
            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler( euler + new Vector3(0, 180, 0));
         //   anim.SetTrigger("turn");
            if (isRight)
                x = 1f;
            else
                x = -1f;
        }
        if (gI.runInput)
        {
            anim.SetFloat("speed",1f,0.15f,Time.deltaTime);
        }
        else if (anim.GetFloat("speed") != 0)
        {
            anim.SetFloat("speed", 0f, 0.15f, Time.deltaTime);
        }
        if (gI.valueX * x > 0)
        {
            if(gI.runInput)
                transform.rotation=Quaternion.Euler(0,20.07f + x * 90f, 0);
            else
                transform.rotation=Quaternion.Euler(0,43.04f + x * 90f, 0);
            anim.SetFloat("direction", gI.valueX*x, 0.15f, Time.deltaTime);
        }
        else if (gI.valueX * x < 0)
        {
            if(gI.runInput)
                transform.rotation = Quaternion.Euler(0,42.425f + x * 90f, 0);
            else
                transform.rotation = Quaternion.Euler(0,36.114f + x * 90f, 0);
            anim.SetFloat("direction", gI.valueX*x, 0.15f, Time.deltaTime);
        }
        else if (anim.GetFloat("direction")!=0)
        {
            anim.SetFloat("direction", 0,0.15f,Time.deltaTime);
        }
        Vector3 newp = transform.position;
        newp.z = 11f;
        transform.position = newp;
    }
}
