using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeInit : MonoBehaviour {

	public SetVolumes setVolumes;

	void Start()
	{
		if (!PlayerPrefs.HasKey("SliderInit"))
		{
			SliderInit();
		}

		float mastervalue = PlayerPrefs.GetFloat("MasterValue");
		float sevalue = PlayerPrefs.GetFloat("SEValue");
		float bgmvalue = PlayerPrefs.GetFloat("BGMValue");

		setVolumes.masterVolume = mastervalue;
		setVolumes.seVolume = sevalue;
		setVolumes.bgmVolume = bgmvalue;
	}

	void SliderInit()
	{
		SliderDeleteInit();
		PlayerPrefs.SetFloat("MasterValue", 0.8f);
		PlayerPrefs.SetFloat("SEValue", 0.8f);
		PlayerPrefs.SetFloat("BGMValue", 0.8f);
		PlayerPrefs.SetInt("SliderInit", 1);
	}

	void SliderDeleteInit()
	{
		PlayerPrefs.DeleteKey("MasterValue");
		PlayerPrefs.DeleteKey("SEValue");
		PlayerPrefs.DeleteKey("BGMValue");
	}
}
