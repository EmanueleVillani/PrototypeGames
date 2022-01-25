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

    private Animator anim;
    public LayerMask toHit;
    public bool isOn = false;
    public Light spotlight;
    void Start()
    {
        anim =GetComponent<Animator>();
        currentLight = totalLight;
        UIManager.instance.Light.value = totalLight;
    }
    RaycastHit[] hits;
    void Update()
    {
        if (isOn)
        {
            //P0(0,0,0) +  D0vector3(0,0,1)*10f = p1(0,0,10)
            //x 0 x 10 = 0
            //y 0.5 x 10 = 5
            //z 0.5 x 10 = 5
            //(0,0.5,0.5)*10f=(0,5,5)
            //transform.position +( transform.forward * 10f);
            //transform.position + transform.rotation*transform.positon2 
            //wordspace globale e quindi somma tutte le transformazione degli antenati (padre padre del padre etc etc ) transform.position
            //local space quella locale senza trasnformazione transform.localposition
            
            hits = Physics.SphereCastAll(transform.position, 2f, transform.forward, 10f, toHit);
            if (hits.Length>0)
            {
                foreach (RaycastHit hit in hits)
                {
                    hit.transform.GetComponentInParent<Enemy>()?.Stun();
                   // hit.transform.GetComponentInParent<MosquitoAnimation>()?.Stun();
                }
            }

            currentLight -= Time.deltaTime * 3;
            UIManager.instance.Light.value = currentLight;
            if (currentLight <= 20)
            {
                anim.SetBool("low", true);
                isLow = true;
            }

            if (currentLight < 0)
            {
                currentLight = 0;
                anim.SetBool("dark", true);
                isLow = false;
                isDark = true;
            }
            if (currentLight > totalLight)
                currentLight = totalLight;
        }
    }
    public void SetSpotLight()
    {
        isOn ^= true;
        spotlight.enabled = isOn;
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position+transform.forward*10f);
    }
}
