using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GatherInput gI;
    public float off =11;

    public GameObject direction;
    public GameObject fireBall;
    public Transform fireBallPoint;
    public Transform looktransform;
    public float fireBallSpeed = 600;
    public Transform gun;
    private void Start()
    {
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Update()
    {
        if (gI.mousedelta.magnitude != 0)
        {
            Vector3 pos = gI.mouseposition;
            pos.z = off;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
        if (gI.fireInput)
        {
            FireBallAttack();
        }
      //  gun.LookAt(transform);
    }

    public void FireBallAttack()
    {
        GameObject ball = Instantiate(fireBall, looktransform.position, Quaternion.identity);
        // ball.transform.parent = null;
        //looktransform.LookAt(transform);
        Vector3 dirCorrection = Vector3.ProjectOnPlane(looktransform.forward, Vector3.back);
        ball.GetComponent<Rigidbody>().AddForce(dirCorrection * fireBallSpeed);

    }
}
