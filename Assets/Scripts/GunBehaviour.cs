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
    MoveByAnimation move;
    public GameObject vfx;
    // Start is called before the first frame update
    void Start()
    {
        move= FindObjectOfType<MoveByAnimation>();
        InventoryManager.Instance.ModifyInventory(Item.Ammo,true,8);
        anim = gI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gI.fireInput)
        {
            //  anim.SetTrigger("fire");
            StartCoroutine(ShootVFX());
            FireBallAttack();
        }
        //Debug.Log(looktransform.position);
        Vector3 pos= looktransform.position;
        pos.z = move.axis;
        looktransform.position = pos;
        Vector3 lpos= looktransform.localPosition;
        lpos.y = 0;
        lpos.z = 0;
        looktransform.localPosition = lpos;
        Debug.DrawLine(looktransform.position ,fireBallPoint.transform.position);
    }
    public void FireBallAttack()
    {
        if (Ammunition == 0)
            return;
        Ammunition--;
        InventoryManager.Instance.ModifyInventory(Item.Ammo,false,1);
        GameObject ball = Instantiate(fireBall, looktransform.position, Quaternion.identity);
        looktransform.transform.LookAt(fireBallPoint.transform.position);
        Vector3 dirCorrection = Vector3.ProjectOnPlane(looktransform.forward, Vector3.back);
        ball.GetComponent<Rigidbody>().AddForce(looktransform.forward * fireBallSpeed);
    }
    public Transform Vfxtrasform;
    IEnumerator ShootVFX()
    {
        GameObject vf = Instantiate(vfx,Vfxtrasform.position,Vfxtrasform.rotation,Vfxtrasform.parent);
        yield return new WaitForSeconds(0.1f);
        DestroyImmediate(vf);
    }
}