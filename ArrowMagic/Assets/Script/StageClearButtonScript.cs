using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClearButtonScript : MonoBehaviour {

	public int StageMax;
	public GameObject Camera1, Camera2, Camera3, Camera4;
	public GameObject Canvas,Image;
	public Image Camera1button, Camera2button, Camera3button, Camera4button;
	public Sprite[] Camera1buttonSprite, Camera2buttonSprite, Camera3buttonSprite, Camera4buttonSprite;

	public void Start()
	{
		Camera1button.sprite = Camera1buttonSprite[0];
		Camera2button.sprite = Camera2buttonSprite[0];
		Camera3button.sprite = Camera3buttonSprite[0];
		Camera4button.sprite = Camera4buttonSprite[1];
	}

	public void OnBackStageSelect()
	{
		SoundManager.PlaySE(10);
		SoundManager.StopSE(6);
		SoundManager.StopSE(7);
		SceneManager.LoadScene(2);
	}

	public void OnReStart()
	{
		SoundManager.TimeSE(6);
		SoundManager.TimeSE(7);
		// 現在のScene名を取得する
		Scene loadScene = SceneManager.GetActiveScene();
		// Sceneの読み直し
		SceneManager.LoadScene(loadScene.name);
	}

	public void OnNextLevel()
	{
		SoundManager.PlaySE(9);
		int SceneIndex = SceneManager.GetActiveScene().buildIndex;
		int stagelevel = PlayerPrefs.GetInt("StageLevel");

		if (stagelevel < SceneIndex && SceneIndex < StageMax) PlayerPrefs.SetInt("StageLevel", SceneIndex-1);

		if (SceneIndex < StageMax) SceneManager.LoadScene(SceneIndex + 1);
		else
		{
			SoundManager.ResetSE();
			SceneManager.LoadScene(1);
		}
	}

	public void OnStageSelect()
	{
		SoundManager.PlaySE(9);
		SoundManager.ResetSE();
		int SceneIndex = SceneManager.GetActiveScene().buildIndex;
		int stagelevel = PlayerPrefs.GetInt("StageLevel");

		if (stagelevel < SceneIndex && SceneIndex < StageMax) PlayerPrefs.SetInt("StageLevel", SceneIndex-1);

		SceneManager.LoadScene(2);
	}

	public void OnBackTitle()
	{
		SoundManager.PlaySE(10);
		SoundManager.ResetSE();
		SceneManager.LoadScene(1);
	}

	public void OnMenu()
	{
		SoundManager.StopSE(6);
		SceneManager.LoadScene(2);
	}

	public void OnCamera1()
	{
		Canvas.transform.parent = Camera1.transform;
		Image.SetActive(false);
		Camera1.SetActive(true);
		Camera2.SetActive(false);
		Camera3.SetActive(false);
		Camera4.SetActive(false);
		Camera1button.sprite = Camera1buttonSprite[1];
		Camera2button.sprite = Camera2buttonSprite[0];
		Camera3button.sprite = Camera3buttonSprite[0];
		Camera4button.sprite = Camera4buttonSprite[0];
	}

	public void OnCamera2()
	{
		Canvas.transform.parent = Camera2.transform;
		Image.SetActive(false);
		Camera1.SetActive(false);
		Camera2.SetActive(true);
		Camera3.SetActive(false);
		Camera4.SetActive(false);
		Camera1button.sprite = Camera1buttonSprite[0];
		Camera2button.sprite = Camera2buttonSprite[1];
		Camera3button.sprite = Camera3buttonSprite[0];
		Camera4button.sprite = Camera4buttonSprite[0];
	}

	public void OnCamera3()
	{
		Canvas.transform.parent = Camera3.transform;
		Image.SetActive(false);
		Camera1.SetActive(false);
		Camera2.SetActive(false);
		Camera3.SetActive(true);
		Camera4.SetActive(false);
		Camera1button.sprite = Camera1buttonSprite[0];
		Camera2button.sprite = Camera2buttonSprite[0];
		Camera3button.sprite = Camera3buttonSprite[1];
		Camera4button.sprite = Camera4buttonSprite[0];
	}

	public void OnCamera4()
	{
		Canvas.transform.parent = Camera4.transform;
		Image.SetActive(true);
		Camera1.SetActive(false);
		Camera2.SetActive(false);
		Camera3.SetActive(false);
		Camera4.SetActive(true);
		Camera1button.sprite = Camera1buttonSprite[0];
		Camera2button.sprite = Camera2buttonSprite[0];
		Camera3button.sprite = Camera3buttonSprite[0];
		Camera4button.sprite = Camera4buttonSprite[1];
	}
}
