using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation= Quaternion.Euler(transform.eulerAngles+Vector3.forward*90f*Time.deltaTime);
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
