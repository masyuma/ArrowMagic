using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	public Slider Master,SE,BGM;
	public Text mValue, sValue, bValue;

	// Use this for initialization
	void Start () {

		if (!PlayerPrefs.HasKey("SliderInit"))
		{
			SliderInit();
		}
		else
		{
			float mastervalue = PlayerPrefs.GetFloat("MasterValue");
			float sevalue = PlayerPrefs.GetFloat("SEValue");
			float bgmvalue = PlayerPrefs.GetFloat("BGMValue");

			Master.value = mastervalue;
			SE.value = sevalue;
			BGM.value = bgmvalue;
		}
	}
	
	// Update is called once per frame
	void Update () {
		mValue.text = " "  + (int)(Master.value * 100);
		sValue.text = " "  + (int)(SE.value * 100);
		bValue.text = " "  + (int)(BGM.value * 100);

		PlayerPrefs.SetFloat("MasterValue", Master.value);
		PlayerPrefs.SetFloat("SEValue", SE.value);
		PlayerPrefs.SetFloat("BGMValue", BGM.value);
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
