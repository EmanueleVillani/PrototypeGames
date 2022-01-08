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
    private Text scoreText;
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
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.gameObject.SetActive(false);
        LoadLevel(0);
    }
    
    private void Update()
    {
        if (PlayerManager.Instance.player != null)
        {
            if (waitSecond >= 0)
            {
                waitSecond -= Time.fixedDeltaTime;
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
        if (level.IsMenu)
        {
            UIManager.instance.SetActiveGameCanvas(false);
        }
        else
        {
            camera.SetActive(false);
            UIManager.instance.SetActiveMainCanvas(false);
        }
        yield return SceneManager.LoadSceneAsync(level.Name, LoadSceneMode.Additive);
        AudioManager.instance.PlayBgMusic(level.Music);
        if (level.IsMenu)
        {
            camera.SetActive(true);
            UIManager.instance.SetActiveMainCanvas(true);
        }
        else
        {
            UIManager.instance.SetActiveGameCanvas(true);
        }
        currentlevel = level;
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
    public GameObject VideoPlayer;
    public void LoadFirstLevel()
    {
        VideoPlayer.SetActive(true);
        Invoke("LoadFirst", 4f);  
    }
    public void LoadFirst()
    {
        LoadLevel(1);
        VideoPlayer.SetActive(false);
    }
    #endregion
    #region GAME MECHANICS
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