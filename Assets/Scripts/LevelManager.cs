using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    public int currentLevel = 0;
    public GameObject player;
    public GameObject Enemies;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(UnloadCurretScene());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(currentLevel);
        }
    }
    public IEnumerator UnloadCurretScene()
    {
        player.SetActive(false);
        Enemies.SetActive(false);
        // SceneManager.SetActiveScene(gameObject.scene);
        // while (SceneManager.GetActiveScene().name!= gameObject.scene.name)
        // {
        //     yield return null;
        // }
        yield return SceneManager.UnloadSceneAsync(levels[currentLevel].Name);
    }
    public IEnumerator LoadScene(string level)
    {
        yield return SceneManager.LoadSceneAsync(level,LoadSceneMode.Additive);
        Enemies.SetActive(true);
        player.SetActive(true);
    }
    public void LoadLevel(int level)
    {
       StartCoroutine(LoadScene(levels[level].Name));
    }
    public void LoadLevel(string level)
    {
        StartCoroutine(LoadScene(level));
    }
    public void LoadLevel(Level level)
    {
        StartCoroutine(LoadScene(level.Name));
    }
}
[System.Serializable]
public class Level
{
    public string Name;
    public Vector3 Position;

    public Level(string n,Vector3 p)
    {
        Name = n;
        Position = p;
    }
}