using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis("Vertical") != 0)
		{
			if (!keyDown)
			{
				if (Input.GetAxis("Vertical") < 0)
				{


					if (index < maxIndex)
					{
						index++;

					}
					else
					{
						index = 0;
						Debug.Log("Play");

					}
				}
				else if (Input.GetAxis("Vertical") > 0)
				{
					if (index > 0)
					{


						index--;

					}
					else
					{
						index = maxIndex;

					}
				}
				keyDown = true;
			}
		}
		else
		{
			keyDown = false;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (index == 0)
			{
				SceneManager.LoadScene("AlphaMain");
				Debug.Log("Premuto Index 0");
			}
			if (index == 1)
			{
				SceneManager.LoadScene("SettingScenes");
				Debug.Log("premutoIndex1");
			}
			if (index == 2)
			{
				Quit();
				//SceneManager.LoadScene(6);
				Debug.Log("premuto index2");
			}
		}
	}

	public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	index = 1;
		//	SceneManager.LoadScene(4);
		//	Debug.Log("Space");
		//	Debug.Log("indice_1");
		//}
		//if (Input.GetKeyUp(KeyCode.Space))
		//{
		//	index = 2;
		//	SceneManager.LoadScene(1);

		//	Debug.Log("indice_2");
		//}
	

}
