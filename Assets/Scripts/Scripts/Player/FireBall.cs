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
            AudioManager.instance.Play("HitSomething");
            other.GetComponent<Enemy>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }

        if (other.tag=="Insect")
        {
            if(damageEffect!=null)
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            AudioManager.instance.Play("HitSomething");
            Destroy(gameObject);
            other.GetComponentInParent<FlyingEnemy>()?.TakeDamage(damageAmount);
            // Debug.Log("COLLIDED");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
    }
}
