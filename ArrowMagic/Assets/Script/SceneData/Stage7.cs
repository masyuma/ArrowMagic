using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7 : MonoBehaviour {

	void Start()
	{
		if (Data.Instance.referer == "Stage7")
		{
			SoundManager.PlaySE(5);
		}
		else
		{
			SoundManager.PlaySE(0);
			SoundManager.PlaySE(7);
		}
		
		Data.Instance.referer = "Stage7";
	}
}
