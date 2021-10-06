using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    private PlayerHealth playerHealth;

    [SerializeField]
    private Slider sliderHealth;

    [SerializeField]
    private float timeToDeath = 0.2f;  //Time between the healthPlayer=0 and the time when the player is destroyed

    private void Awake()
    {       
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!gameManagerInstance)
            gameManagerInstance = this;


        sliderHealth.maxValue = playerHealth.maxHealth;
        sliderHealth.value = playerHealth.currentHealth;


    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth)
            return;

        sliderHealth.value = playerHealth.currentHealth;

    }


    public void DestroyPlayer()
    {

        StartCoroutine(_Death(timeToDeath));

    }


    IEnumerator _Death(float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(playerHealth.gameObject);

    }




}
