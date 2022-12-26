using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevel : MonoBehaviour
{
    public GameObject camerablock;
    public GameObject camerago;
    public GameObject Spawner1, Spawner2;
    private void Awake()
    {
        camerago.SetActive(true);
        camerablock.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.secondEvent += SecondPhase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"&& Spawner1!=null)
        {
            PlayerManager.Instance.level1done = true;
            GameManager.Instance.LoadSecondLevel();
            Destroy(Spawner1.gameObject);
        }
    }

    public void SecondPhase()
    {
        UIManager.instance.ShowKillText();
        camerago.SetActive(false);
        camerablock.SetActive(true);
        Spawner2.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.Instance.secondEvent -= SecondPhase;
    }
}
