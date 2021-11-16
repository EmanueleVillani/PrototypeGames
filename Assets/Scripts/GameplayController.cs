using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
   
    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }


    public void QuitGame()
    {
      //  SceneManager.LoadScene("MainMenu");



    }




}
