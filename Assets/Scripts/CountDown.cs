using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    private GameManager1 gm;
    public float waitSecond;
    public int waitIntSec;
    public Text textTime;
    public GameObject addTime;




    void Start()
    {
        //UpdateText();

    }
    private void FixedUpdate()
    {
        if (waitSecond >= 0)
        {
            waitSecond -= Time.fixedDeltaTime;
            waitIntSec = (int)waitSecond;
            textTime.text = waitIntSec.ToString();

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
   


    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "addTime")
    //    {
    //        Debug.Log("Sto Toccando");
    //        waitSecond += 20;
    //        Destroy(collision.gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "clessidra")
        {
            Debug.Log("Sto Toccando");
            waitSecond += 20;
            Destroy(other.gameObject);
        }
    }

}
