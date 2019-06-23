using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonScript : MonoBehaviour {

	public GameObject CreditsUI;

	public void OnStart()
	{
		SoundManager.PlaySE(9);
		SoundManager.TimeSE(8);
		SceneManager.LoadScene("StageSelect");
	}

	public void OnQuit()
	{
		SoundManager.PlaySE(10);
		Application.Quit();

#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
	}

	public void OnCredits()
	{
		SoundManager.PlaySE(9);
		CreditsUI.SetActive(true);
	}

	public void OnBack()
	{
		SoundManager.PlaySE(10);
		CreditsUI.SetActive(false);
	}
}
