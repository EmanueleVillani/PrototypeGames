using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField]
    private SpotlightScript spotLight;

    [SerializeField]
    private int amountLight = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            spotLight.AddLight(amountLight);

            Destroy(gameObject, 0.2f);


        }



    }
}


