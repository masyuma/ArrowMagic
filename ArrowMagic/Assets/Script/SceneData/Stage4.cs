using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : MonoBehaviour {

	void Start()
	{
		if (Data.Instance.referer == "Stage4")
		{
			SoundManager.PlaySE(5);
		}
		else
		{
			SoundManager.PlaySE(0);
			SoundManager.PlaySE(6);
		}
		
		Data.Instance.referer = "Stage4";
	}
}
