using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MainCanvas,GameCanvas;
    public GameObject[] MainPanels;
    public GameObject[] GamePanels;
    public Text Timer;
    public Slider Light, Health, Stamina;
    
    public Text GameOverMessage;
    public GameObject gameoverpanel;
    private void Awake()
    {
        if(instance!=this || instance == null)
        {
            instance = this;
        }
    }

    public void SetActiveMainCanvas(bool b)
    {
        foreach (GameObject g in MainPanels)
        {
            g.SetActive(false);
        }

        MainCanvas.SetActive(b);
        MainPanels[0].SetActive(b);
    }
    public void SetActiveGameCanvas(bool b)
    {
        GameCanvas.SetActive(b);
    }

    public void SetActiveGameOverPanel(bool b,string testo)
    {
        gameoverpanel.SetActive(b);
        GameOverMessage.text = testo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}
