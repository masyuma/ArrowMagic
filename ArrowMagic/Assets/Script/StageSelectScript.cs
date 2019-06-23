using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour {

	//public int a;
	public Button[] StageSelectButton;

	// Use this for initialization
	public void Start () {

		int stagelevel = PlayerPrefs.GetInt("StageLevel");
		for(int i = stagelevel + 1; i < StageSelectButton.Length; i++)
		{
			StageSelectButton[i].interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStageSelect(int i)
	{
		SoundManager.PlaySE(9);
		SoundManager.StopSE(8);
		SceneManager.LoadScene(i + 2);
	}

	public void OnBack()
	{
		SoundManager.PlaySE(10);
		SceneManager.LoadScene(1);
	}
}
