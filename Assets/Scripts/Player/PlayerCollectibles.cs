using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class PlayerCollectibles : MonoBehaviour
{

    public int gemNumber;
    private Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        

        textComponent = GameObject.FindGameObjectWithTag("GemUI").GetComponentInChildren<Text>();
        UpdateText();
    }

    // Update is called once per frame
    void UpdateText()
    {
        //convertiamo il numero intero in una stringa della raccolta della gemma
        textComponent.text = gemNumber.ToString();
    }
    public void GemCollected()
    {
        gemNumber += 1;
        UpdateText();
    }
}
