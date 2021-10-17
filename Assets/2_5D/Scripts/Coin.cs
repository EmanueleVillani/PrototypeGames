using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 50;
    public Item m_self;

    void Update()
    {
        //Rotate The Coin
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.numberOfCoins++;//Increment the number of coins
            InventoryManager.Instance.ModifyInventory(m_self, true);
            Destroy(gameObject);//destroy the coin
        }
    }
}
