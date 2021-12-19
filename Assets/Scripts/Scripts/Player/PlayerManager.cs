using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;

    public  int currentHealth = 100;
    public Slider healthBar;
    public Slider staminaBar;

    public static bool gameOver;
    public static bool winLevel;

    public GameObject gameOverPanel;

    public float timer = 0;

    public MoveByAnimation player;

    private int killedCount = 0;

    public static PlayerManager instancePlayerManager;


    [SerializeField]
    private Text killedCount_Text;

    [SerializeField]
    private TMP_Text killedCount_Text_GameOverPanel;


    private void Awake()
    {
       
        if (instancePlayerManager == null)
        {
            instancePlayerManager = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {

            Destroy(gameObject);
        }


    }



    void Start()
    {
       

        numberOfCoins = 0;
        gameOver = winLevel = false;

        currentHealth = 100;

        gameOverPanel.SetActive(false);

        killedCount_Text.gameObject.SetActive(true);
        killedCount = 0;

        killedCount_Text.text = "Killed: " + killedCount;

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        currentHealth = 100;
        SceneManager.LoadScene(gameObject.scene.name);
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("SettingScenes");
    }
    void Update()
    {
        //Display the number of coins
        numberOfCoinsText.text = "coins:" + numberOfCoins;

        //Update the slider value
        healthBar.value = currentHealth;
        staminaBar.value = player.stamina;
        //game over
        if (currentHealth < 0  && gameOver == false)
       {
           gameOver = true;
           //Invoke("Restart", 5);
           gameOverPanel.SetActive(true);
           gameOverPanel.GetComponent<Animator>().SetTrigger("GameOver");
            //currentHealth = 100;
            killedCount_Text_GameOverPanel.text = "Killed: " + killedCount;
            killedCount_Text.gameObject.SetActive(false);

            GameManager.gameManagerInstance.gameOver = true;
            GameManager.gameManagerInstance.scoreKilled = killedCount;
       }

        if (currentHealth < 25)
        {
            if (!AudioManager.instance.IsPlaying("LifeUnder"))
            {
                float time = AudioManager.instance.GetTime("BgMusic");
                AudioManager.instance.Play("LifeUnder", time);
            }
        }
        else
        {
            if (AudioManager.instance.IsPlaying("LifeUnder"))
                AudioManager.instance.Stop("LifeUnder");
        }
        // if(FindObjectsOfType<Enemy>().Length ==0)
        // {
        //     //Win Level
        //     winLevel = true;
        //     timer += Time.deltaTime;
        //     if(timer > 5)
        //     {
        //         /**
        //         int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        //         if (nextLevel == 4)
        //             SceneManager.LoadScene(0);
        //                         if(PlayerPrefs.GetInt("ReachedLevel", 1) < nextLevel)
        //                             PlayerPrefs.SetInt("ReachedLevel", nextLevel);
        //
        //                         SceneManager.LoadScene(nextLevel);
        //         **/
        //         SceneManager.LoadScene(0);
        //     }
        // }
    }

    public void AddCount()
    {
        killedCount++;

        killedCount_Text.text = "Killed: " + killedCount;



    }
   

}
