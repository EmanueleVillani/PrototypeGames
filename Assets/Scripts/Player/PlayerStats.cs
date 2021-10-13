using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public bool canTakeDamege = true;//un piccolo tempo di recupero in cui non è possibile subire danni.

    private Animator anim;

    public float maxHealth;
    public float health;

    private PlayerAttackControls pAC;


    void Start()
    {
        anim = GetComponentInParent<Animator>();

        // imposto la salute max del player per iniziare in piena salute
        health = maxHealth;

        pAC = GetComponentInParent<PlayerAttackControls>();

    }


    // creo una funzione pubblica che può danneggiare
    // il giocatore e diminuire la salute.
    public void TakeDemage(float damage)
    {

        if (canTakeDamege)
        {
            health -= damage;

            anim.SetBool("Damage",true);

            pAC.ResetAttack();
            //All'interno, la funzione ridurrà la salute in base al danno.
            if (health <= 0)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                Debug.Log("Il Player e morto");
            }

            StartCoroutine(DamagePrevention());
       
        }
    }
    //La sintassi è un po' strana.
    //creato la funzione enumeratore.
    private IEnumerator DamagePrevention()
    {

        //All'interno,  la variabile bool
        //su false, quindi non possiamo prendere l'immagine.
        canTakeDamege = false;
        yield return new WaitForSeconds(0.15f);//attendi la fine del frame aspetto qualche  secondo.


        //controlliamo se il giocatore è ancora vivo,
        //se è vero, quindi impostiamo la variabile bool su true cosi
        //possiamo riprendere l'immahgine
        if (health > 0)
        {
            canTakeDamege = true;
            anim.SetBool("Damage", false);
        }
        else
        {
            // Player Death Animation
            anim.SetBool("Death", true);
        }
             
    }
}
