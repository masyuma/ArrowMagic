using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //各シーンのリスタートやアプリケーションの終了を行うスクリプト
{
	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		TapScreen();

		//Escキーでアプリケーション終了
		if (Input.GetKey(KeyCode.Escape))
		{
            Application.Quit();
		}
	}

	void TapScreen()
	{
		if (SceneManager.GetActiveScene().name == "Header")
		{
			if (Input.GetMouseButton(0))
			{
				SoundManager.TimeSE(8);
				SceneManager.LoadScene(1);
			}
		}
	}
}
