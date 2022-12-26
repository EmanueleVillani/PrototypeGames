using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MainCanvas,GameCanvas;
    public GameObject[] MainPanels;
    public GameObject[] GamePanels;
    public Text Timer;
    public GameObject killtext, runtext;
    public Slider Light, Health, Stamina;
    
    public Text GameOverMessage;
    public GameObject gameoverPanel;
    public GameObject pausePanel;
    public GameObject loadingPanel;
    public GameObject Logo;
    public Text Scoretext;
    private PointerEventData m_PointerEventData;

    private void Awake()
    {
        if(instance!=this || instance == null)
        {
            instance = this;
        }
    }
    public void ShowRunText()
    {
        StartCoroutine(CoShowRunText());
    }
    public void ShowKillText()
    {
        StartCoroutine(CoShowKillText());
    }
    public IEnumerator CoShowRunText()
    {
        runtext.SetActive(true);
        yield return new WaitForSeconds(5f);
        runtext.SetActive(false);
    }
    public IEnumerator CoShowKillText()
    {
        killtext.SetActive(true);
        yield return new WaitForSeconds(5f);
        killtext.SetActive(false);
    }
    public bool CheckHitUi()
    {
        GraphicRaycaster raycaster= GameCanvas.GetComponent<GraphicRaycaster>();
        m_PointerEventData = new PointerEventData(EventSystem.current);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(m_PointerEventData, results);
        return results.Count>0;
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
        Cursor.visible = !b;
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
