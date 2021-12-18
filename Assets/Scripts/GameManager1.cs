using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instanceGameManager;



    /*
      * Spawner Nemici
    */
    float spawnTimer;
    float spawnRate = 3f; // e la frequenza di spawn ogni 3 secondi
    public GameObject nemici;
    /*
      * Timer ADD Time
    */
    public GameObject textTime; // Testo del conto alla rovescio
    public GameObject addTimer; // Aumento del Tempo

    [SerializeField]
    private GameObject gameOverPanel;

    private GameObject player;

   


    private void Awake()
    {
        if (instanceGameManager == null)
            instanceGameManager = this;
        else
        {
            Destroy(gameObject);

        }


    }



    // Start is called before the first frame update
    void Start()
    {

        gameOverPanel.SetActive(false);



    }

  

    public void GameOver()
    {

        gameOverPanel.SetActive(true);

        GameObject.FindWithTag("Insect").GetComponent<FlyingEnemy>().enabled = false;

    }

    // DA Sistemare 

    // void Update()
    //{
    //    spawnTimer += Time.deltaTime;
    //    if(spawnTimer >= spawnRate)
    //    {
    //       // spawnTimer -= spawnRate;
    //        Vector2 spawnPosi = new Vector2(-30f ,Random.Range(-1f, 2f));
    //        Instantiate(nemici, spawnPosi, Quaternion.identity);
    //    }
        

    //}


}
