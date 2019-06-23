using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInit : MonoBehaviour {

	void Start()
	{
		if (!PlayerPrefs.HasKey("Init"))
		{ // "Init"のキーが存在しない場合はチュートリアルパネルを表示
			SaveDataInitialize(); // セーブデータを初期化
		}
	}

	private void Update()
	{
		Reset();
	}

	void SaveDataInitialize()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("Init", 1); // ”Init”のキーをint型の値(1)で保存
	}

	void Reset()
	{
		if (Input.GetKey(KeyCode.Delete))
		{
			SaveDataInitialize();
		}
	}
}
