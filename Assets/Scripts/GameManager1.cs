using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instanceGameManager;


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


}
