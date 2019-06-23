using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
	void Start()
	{
		SoundManager.EnrouteSE(8);

		Data.Instance.referer = "Title";
	}
}
