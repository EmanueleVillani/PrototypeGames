using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float rotationSpeed = 50;
    public Item m_self;
    public int howmany = 1;

    void Update()
    {
        //Rotate The Coin
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            InventoryManager.Instance.ModifyInventory(m_self, true, howmany);
            switch (m_self)
            {
                case Item.Gem:
                //PlayerManager.numberOfCoins+=howmany;//Increment the number of coins
                Destroy(gameObject);//destroy the coin
                    break;
                case Item.Ammo:
                    FindObjectOfType<GunBehaviour>().Ammunition += howmany;
                Destroy(gameObject);//destroy the coin
                    break;
                case Item.health:
                    if (PlayerManager.Instance.currentHealth < 100)
                    {
                        PlayerManager.Instance.currentHealth += howmany;
                        Destroy(gameObject);//destroy the coin
                    }
                    break;
                case Item.battery:
                    SpotlightScript light= FindObjectOfType<SpotlightScript>();
                    light.AddLight(howmany);
                    Destroy(gameObject);//destroy the coin
                    break;
                case Item.time:
                    GameManager.Instance.waitSecond += howmany;
                    Destroy(gameObject);//destroy the coin
                    break;
            }
        }
    }
}
