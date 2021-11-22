using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpotlightScript : MonoBehaviour
{
    [SerializeField]
    private int totalLight=100;
    private float currentLight;

    private bool isDark;
    private bool isLow;

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
        currentLight -= Time.deltaTime*3;

        lightBar.value = currentLight;

        if (currentLight <= 20) { 
            
            anim.SetBool("low",true);

            isLow = true;

        }


        if (currentLight < 0)
        {
            currentLight = 0;

            anim.SetBool("dark",true);

            isLow = false;

            isDark = true;
        }


        if (currentLight > totalLight)
            currentLight = totalLight;


 
    }


  public void AddLight( int amountLight)
    {
        currentLight += amountLight;


        if (isDark)
        {
            isDark = false;
            
            anim.SetBool("dark",false);
            anim.SetBool("low", false);


        }
        else if(isLow)
        {
            isLow = false;

            anim.SetBool("low",false);

        }
        else
        {

        }

    }



}
