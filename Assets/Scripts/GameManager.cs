using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public bool gameOver;
    public Text scoreText;
    public int scoreKilled;

    #region Level variables
    public List<Level> levels;
    public Level currentlevel;
    //public GameObject Canvas, CanvasPlayer;
    public GameObject camera;
    //public GameObject CutsceneCanvas;
    public PlayableDirector director;
    #endregion
    #region Timer variables
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
                if (!paused)
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
        LoadLevel(currentlevel);
    }
    public IEnumerator LoadLevel(Level level)
    {
        yield return StartCoroutine(UnloadCurretScene());// Unload old level
        yield return StartCoroutine(LoadScene(level));//load new level
    }
    public void LoadLevel(int x)
    {
      StartCoroutine(LoadLevel(levels[x]));
    }
    public GameObject VideoPlayer1, VideoPlayer2, VideoPlayer3;
    public void LoadFirstLevel()
    {
        VideoPlayer1.SetActive(true);
        Invoke("LoadFirst", 4f);
    }
    public void LoadSecondLevel()
    {
        VideoPlayer2.SetActive(true);
        Invoke("LoadSecond", 4f);
    }
    public void LoadSecond()
    {
      //  LoadLevel(3);
        VideoPlayer2.SetActive(false);
    }
    public void LoadFirst()
    {
        LoadLevel(2);
        VideoPlayer1.SetActive(false);
    }
    #endregion
    #region GAME MECHANICS
    public void Pause()
    {
        UIManager.instance.pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
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