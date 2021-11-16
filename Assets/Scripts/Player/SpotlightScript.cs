using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpotlightScript : MonoBehaviour
{
    [SerializeField]
    private int totalLight=30;

    private float currentLight;

    [SerializeField]
    private Slider lightBar;

    private Animator anim;


    void Start()
    {
      anim=GetComponent<Animator>();

        currentLight = totalLight;

        lightBar.value = totalLight;

        
    }

    void Update()
    {
        currentLight -= Time.deltaTime;

        lightBar.value = currentLight;

        if (currentLight <= 25)
            anim.SetTrigger("Low");

        if(currentLight < 0)
        {
            currentLight = 0;

            anim.SetTrigger("Dark");
        }


        
    }
}
