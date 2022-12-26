using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public bool gameOver;
    public Text scoreText;
    public int scoreKilled;
    public event UnityAction secondEvent;
    #region Level variables
    public List<Level> levels;
    public Level currentlevel;
    //public GameObject Canvas, CanvasPlayer;
    public GameObject camera;
    //public GameObject CutsceneCanvas;
    public PlayableDirector director;
    #endregion
    #region Timer variables
    public float waitTotal;
    public float waitSecond;
    public int waitIntSec;
    #endregion
    #region SCORE
    private void Awake()
    {
        if (Instance == null && Instance!=this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.gameObject.SetActive(false);
        LoadLevel(0);
    }
    public bool go = false;
    bool paused = false;
    private void Update()
    {
        if (PlayerManager.Instance.player != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!paused && Time.timeScale!=0)
                    Pause();
                else 
                    Resume();
            }

            if(!go)
                go = (PlayerManager.Instance.player.gI.valueX > 0);

            if (waitSecond >= 0 && go)
            {
                waitSecond -= Time.deltaTime;
                waitIntSec = (int)waitSecond;
                UIManager.instance.Timer.text = waitIntSec.ToString();
                //textTime.text = waitIntSec.ToString();
                if (waitSecond <= 0)
                {
                    AudioManager.instance.PlayBgMusic("Death");
                    AudioManager.instance.Stop("LifeUnder");
                    UIManager.instance.SetActiveGameOverPanel(true, @"Sei morto male");
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    // GameManager.Instance.Pause();
                    gameOver = true;
                }
            }
        }
    }
    #endregion
    #region LEVELS
    public IEnumerator UnloadCurretScene()
    {
        if (!string.IsNullOrEmpty(currentlevel.Name))
        {
            yield return SceneManager.UnloadSceneAsync(currentlevel.Name);
        }
    }
    public IEnumerator LoadScene(Level level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level.Name, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            UIManager.instance.SetLoadingActive(true);
            yield return null;
        }
        UIManager.instance.SetLoadingActive(false);

        AudioManager.instance.PlayBgMusic(level.Music);
        if (level.IsMenu)
        {
            camera.SetActive(true);
            UIManager.instance.SetActiveGameCanvas(false);
            UIManager.instance.SetActiveMainCanvas(true);
        }
        else
        {
            camera.SetActive(false);
            UIManager.instance.SetActiveMainCanvas(false);
            UIManager.instance.SetActiveGameCanvas(true);
        }
        currentlevel = level;
        yield return null;
    }
    public void ReloadLevel()
    {
        StartCoroutine(ReLoadLevel(currentlevel));
    }
    public IEnumerator LoadLevel(Level level)
    {
        UIManager.instance.SetLoadingActive(true);
        yield return StartCoroutine(UnloadCurretScene());// Unload old level
        yield return StartCoroutine(LoadScene(level));//load new level
    }
    public IEnumerator ReLoadLevel(Level level)
    {
        UIManager.instance.SetLoadingActive(true);
        yield return SceneManager.UnloadSceneAsync(currentlevel.Name);
        yield return StartCoroutine(LoadScene(level));//load new level
    }
    public void LoadLevel(int x)
    {
      StartCoroutine(LoadLevel(levels[x]));
    }
    public GameObject VideoPlayer1, VideoPlayer2, VideoPlayer3;
    Coroutine co1,co2,co3;
    public void LoadFirstLevel()
    {
        UIManager.instance.Timer.text = GameManager.Instance.waitTotal.ToString();
        GameManager.Instance.waitSecond = (int)GameManager.Instance.waitTotal;
        VideoPlayer1.SetActive(true);
        co1 = StartCoroutine(CoLoadFirst());
    }
    public IEnumerator CoLoadFirst()
    {
        yield return new WaitForSeconds(60);
        LoadFirst();
    }
    public IEnumerator CoLoadSecond()
    {
        yield return new WaitForSeconds((float)VideoPlayer2.GetComponentInChildren<UnityEngine.Video.VideoPlayer>(true).clip.length);
        LoadSecond();
    }
    public IEnumerator CoLoadThird()
    {
        yield return new WaitForSeconds((float)VideoPlayer3.GetComponentInChildren<UnityEngine.Video.VideoPlayer>(true).clip.length);
        LoadThird();
    }
    public void SkipFirst()
    {
        StopCoroutine(co1);
        LoadFirst();
    }
    public void SkipSecond()
    {
        StopCoroutine(co2);
        LoadSecond();
    }
    public void SkipThird()
    {
        StopCoroutine(co3);
        LoadThird();
    }
    public void LoadSecondLevel()
    {
        VideoPlayer2.SetActive(true);
        Cursor.visible = true;
        AudioManager.instance.StopBg();
        AudioManager.instance.Stop("LifeUnder");
        co2 = StartCoroutine(CoLoadSecond());
    }
    public void LoadEnd()
    {
        VideoPlayer3.SetActive(true);
        AudioManager.instance.StopBg();
        AudioManager.instance.Stop("LifeUnder");
        co3 = StartCoroutine(CoLoadThird());
    }
    public void LoadSecond()
    {
        //  LoadLevel(3);
        Debug.Log("second");
        secondEvent?.Invoke();
        AudioManager.instance.PlayBgMusic("MosquitoMaster");
        VideoPlayer2.SetActive(false);
        Cursor.visible = false;
    }
    public void LoadThird()
    {
        //  LoadLevel(3);
        Debug.Log("third");
        VideoPlayer3.SetActive(false);
        Resume();
        LoadLevel(0);
    }
    public void LoadFirst()
    {
        UIManager.instance.Timer.text = GameManager.Instance.waitTotal.ToString();
        GameManager.Instance.waitSecond = (int)GameManager.Instance.waitTotal;
        PlayerManager.Instance.inv = false;
        PlayerManager.Instance.currentHealth = 100;
        UIManager.instance.ShowRunText();
        LoadLevel(1);
        VideoPlayer1.SetActive(false);
    }
    #endregion
    #region GAME MECHANICS
    public void Pause()
    {
        Cursor.visible = true;
        UIManager.instance.pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Cursor.visible = false;
        UIManager.instance.pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    #endregion
}
[System.Serializable]
public class Level
{
    public string Name;
    public string Music;
    public Vector3 Position;
    public bool IsMenu=true;

    public Level(string n, Vector3 p)
    {
        Name = n;
        Position = p;
    }
}