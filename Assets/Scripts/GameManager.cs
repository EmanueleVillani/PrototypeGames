using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

   // [HideInInspector]
    public bool gameOver;

   
    private Text scoreText;

    public int scoreKilled;

    private void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }


    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.gameObject.SetActive(false);
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!(SceneManager.GetActiveScene().name == "SampleScene")  || !gameOver)
            return;

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        if (gameOver)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Killed: " + scoreKilled;

        }
        else
        {
            scoreText.gameObject.SetActive(false);
        }

    }





}
