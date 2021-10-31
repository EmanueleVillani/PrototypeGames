using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuHorror : MonoBehaviour
{

    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;
    

    public void SetVolume()
    {
        mixer.SetFloat("volume", volumeSlider.value);



    }
    private void strt()
    {
        mixer.GetFloat("volume", out value );
        volumeSlider.value = value;
    }


    public void PlayMenuHorror()
    {
        Debug.Log("PlayHorror");
        SceneManager.LoadScene(6);
    }
    public void OpzioniMenuHorror()
    {
        Debug.Log("OpzioniHorror");
    }
    public void ExitMenuHorror()
    {
        Debug.Log("Exit");
        //SceneManager.LoadScene(2);
        Application.Quit();
    }

    public void CreditHorror()
    {
        Debug.Log("Credit");
        SceneManager.LoadScene(5);
    }

    public void CreditHorrorBack()
    {
        Debug.Log("CreditBeck");
        SceneManager.LoadScene(4);
    }
    public void Setting()
    {
        Debug.Log("Setting");
        SceneManager.LoadScene(4);
    }
}
