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
    public GameObject gameoverPanel;
    public GameObject loadingPanel;
    public GameObject Logo;
    public Text Scoretext;
    private void Awake()
    {
        if(instance!=this || instance == null)
        {
            instance = this;
        }
    }
    public void ToggleLogo(bool b)
    {
        Logo.SetActive(!b);
    }
    public void SetLoadingActive(bool b)
    {
        loadingPanel.SetActive(b);
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
        gameoverPanel.SetActive(b);
        GameOverMessage.text = testo;
    }
    public void SetLastScore(int x)
    {
        Scoretext.gameObject.SetActive(true);
        Scoretext.text = "Killed: " + x;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}
