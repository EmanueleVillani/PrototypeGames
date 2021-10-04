﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
    private PlayerMoveControls pMc;
    private GatherInput gI;
    private Animator anim;

    public bool attacStaerted = false;

    public PolygonCollider2D polyCol;
    // Start is called before the first frame update
    void Start()
    {
        pMc = GetComponent<PlayerMoveControls>();
        gI = GetComponent<GatherInput>();
        anim = GetComponent<Animator>(); // ANIMATOR 
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    private void Attack()
    {
        // premi il pulsante attack
        if (gI.tryAttack)
        {
            if (attacStaerted || pMc.hasControl == false || pMc.knokBack)
                return;

            anim.SetBool("Attack", true);
            attacStaerted = true;

        }
    }

    public void ActiveteAttack()
    {
        polyCol.enabled = true;
    }
    public void ResetAttack()
    {
        anim.SetBool("Attack", false);
        gI.tryAttack = false;
        attacStaerted = false;
        polyCol.enabled = false;
    }
}
