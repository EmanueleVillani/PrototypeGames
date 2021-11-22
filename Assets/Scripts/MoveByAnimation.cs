﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class MoveByAnimation : MonoBehaviour
{
    Animator anim;
    GatherInput gI;
    public CinemachineVirtualCamera virtualcamera;
    public GameObject target;
    public bool isRight = true;
    public bool isrunning = false;
    public float stamina = 20f;
    public float capstamina = 20f;
    public TwoBoneIKConstraint otherarm;
    public ChainIKConstraint aimarm;
    public MultiAimConstraint headaim;
    public Collider weapon;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gI = GetComponent<GatherInput>();
    }
    float x = 1f;
    float cameravalue =0;
    // Update is called once per frame
    void Update()
    {
        isrunning =gI.runInput;

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
        if (gI.valueX != 0)
        {
            if(isRight)
                cameravalue += x * 4 * Time.deltaTime;
            else
                cameravalue -= x * 4 * Time.deltaTime;

            if (cameravalue > 8)
                cameravalue = 8f;
        }
        else
            cameravalue = 0;

       // virtualcamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.up * 8f + Vector3.forward * gI.valueX * x * cameravalue;

        if (/**Input.GetKeyDown(KeyCode.Mouse1) &&**/ gI.tryAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
        {
            transform.rotation = Quaternion.Euler(0, 36.917f+x*90f, 0);
            otherarm.weight = 0f;
            aimarm.weight = 0f;
            headaim.weight = 0f;
            anim.SetTrigger("attack");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
        {
            otherarm.weight = 0f;
            aimarm.weight = 0f;
            headaim.weight = 0f;
            return;
        }
        //staminacheck
        if (gI.valueX != 0 && isrunning)
        {
            if (stamina >= 0)
                stamina -= Time.deltaTime * 2;
            else
                isrunning = false;
        }
        else if (gI.valueX == 0 || !isrunning)
        {
            if (stamina < capstamina)
                stamina += Time.deltaTime * 2;
            else
                stamina = capstamina;
        }

        if (isrunning)
        {
            anim.SetFloat("speed",1f,0.15f,Time.deltaTime);
        }
        else if (anim.GetFloat("speed") != 0)
        {
            anim.SetFloat("speed", 0f, 0.15f, Time.deltaTime);
          
        }
        if (gI.valueX * x > 0)
        {
            if(isrunning)
                transform.rotation=Quaternion.Euler(0,20.07f + x * 90f, 0);
            else
                transform.rotation=Quaternion.Euler(0,40.04f + x * 90f, 0);
            anim.SetFloat("direction", gI.valueX*x, 0.15f, Time.deltaTime);
        }
        else if (gI.valueX * x < 0)
        {
            if(isrunning)
                transform.rotation = Quaternion.Euler(0,42.425f + x * 90f, 0);
            else
                transform.rotation = Quaternion.Euler(0,36.114f + x * 90f, 0);
            anim.SetFloat("direction", gI.valueX*x, 0.15f, Time.deltaTime);
        }
        else if (anim.GetFloat("direction")!=0)
        {
            anim.SetFloat("direction", 0,0.15f,Time.deltaTime);
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Melee") && otherarm.weight == 0 && aimarm.weight == 0 && headaim.weight == 0)
        {
            otherarm.weight = 1f;
            aimarm.weight = 1f;
            headaim.weight = 0.5f;
        }

    }

    public void LateUpdate()
    {
        Vector3 newp = transform.position;
        newp.z = 11f;
        transform.position = newp;
    }
    
    public void EnableHit()
    {
        weapon.enabled = true;
    }

    public void DisableHit()
    {
        weapon.enabled = false;
    }
}
