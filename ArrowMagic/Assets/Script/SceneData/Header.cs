using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header : MonoBehaviour
{
	void Start()
	{
		SoundManager.PlaySE(8);

		Data.Instance.referer = "Header";
	}
}
