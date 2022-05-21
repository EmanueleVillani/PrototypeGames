using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int currentHealth = 100;

    public static bool gameOver;
    public static bool winLevel;
    public int conditionVictory = 3; // CONDIZIONE DI VITTORIA
    public GameObject gameOverPanel;

    public float timer = 0;

    public MoveByAnimation player;

    public static PlayerManager Instance;
    public bool level1done = false;
    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameOver = winLevel = false;

        currentHealth = 100;

        UIManager.instance.SetActiveGameOverPanel(false,"");

       // killedCount_Text.gameObject.SetActive(true);
        //killedCount = 0;

        //killedCount_Text.text = "Killed: " + killedCount;

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    void Update()
    {
        //Update the slider value
        if (player != null)
        {
            UIManager.instance.Health.value = currentHealth;
            UIManager.instance.Stamina.value = player.stamina;
            if (winLevel)
                return;

            if (InventoryManager.Instance.HowMany(Item.kill) >= conditionVictory)
            {
                winLevel = true;
                AudioManager.instance.PlayBgMusic("Victory");
                UIManager.instance.SetLastScore(InventoryManager.Instance.HowMany(Item.kill));
                UIManager.instance.SetActiveGameOverPanel(true, "Sei Sopravvissuto!");
                GameManager.Instance.Pause();
                return;
            }
            //game over
            if (currentHealth < 0 && gameOver == false)
            {
                gameOver = true;
                AudioManager.instance.PlayBgMusic("Death");
                AudioManager.instance.Stop("LifeUnder");
                UIManager.instance.SetLastScore(InventoryManager.Instance.HowMany(Item.kill));
                UIManager.instance.SetActiveGameOverPanel(true, @"Sei morto male");
                GameManager.Instance.Pause();
                GameManager.Instance.gameOver = true;
            }

            if (currentHealth < 25 && gameOver == false)
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
        }
    }
   
    public void ResetPlayer()
    {
        gameOver = winLevel = false;
        UIManager.instance.SetActiveGameOverPanel(false, "");
        GameManager.Instance.go = false;
        currentHealth = 100;
        UIManager.instance.Timer.text = ""+60;
        level1done = false;
        InventoryManager.Instance.HardSetInventory(Item.kill,0);
        InventoryManager.Instance.HardSetInventory(Item.Gem,0);
        InventoryManager.Instance.HardSetInventory(Item.Ammo, 16);

        //  killedCount_Text.text = "Killed: " + killedCount;
    }

}
