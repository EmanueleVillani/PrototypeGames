using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public int Ammunition = 8;
    public GameObject fireBall;
    public Transform fireBallPoint;
    public Transform looktransform;
    public float fireBallSpeed = 600;
    public GatherInput gI;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.Instance.ModifyInventory(Item.Ammo,true,8);
        anim = gI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gI.fireInput)
        {
          //  anim.SetTrigger("fire");
            FireBallAttack();
        }
    }
    public void FireBallAttack()
    {
        if (Ammunition == 0)
            return;
        Ammunition--;
        InventoryManager.Instance.ModifyInventory(Item.Ammo,false,1);
        GameObject ball = Instantiate(fireBall, looktransform.position, Quaternion.identity);
        // ball.transform.parent = null;
        //looktransform.LookAt(transform);
        Vector3 dirCorrection = Vector3.ProjectOnPlane(looktransform.forward, Vector3.back);
        ball.GetComponent<Rigidbody>().AddForce(dirCorrection * fireBallSpeed);
    }

}