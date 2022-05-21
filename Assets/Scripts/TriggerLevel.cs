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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerManager.Instance.level1done = true;
            GameManager.Instance.LoadSecondLevel();
            Destroy(gameObject);
            camerago.SetActive(false);
            camerablock.SetActive(true);
            Spawner1.SetActive(false);
            Spawner2.SetActive(true);
        }
    }
}
