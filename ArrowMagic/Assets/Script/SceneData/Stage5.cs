using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : MonoBehaviour {

	void Start()
	{
		if (Data.Instance.referer == "Stage5")
		{
			SoundManager.PlaySE(5);
		}
		else
		{
			SoundManager.PlaySE(0);
			SoundManager.PlaySE(7);
		}
		
		Data.Instance.referer = "Stage5";
	}
}
