using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject damageEffect;
    public int damageAmount = 40;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(damageEffect!=null)
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            other.GetComponent<Enemy>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }

        if (other.tag=="Insect")
        {
            if(damageEffect!=null)
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            Destroy(gameObject);
            other.GetComponent<EnemyHealth>()?.TakeDamage(damageAmount);
            // Debug.Log("COLLIDED");
        }
    }
}
