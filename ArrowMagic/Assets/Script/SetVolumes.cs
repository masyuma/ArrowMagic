using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolumes : MonoBehaviour
{
	[SerializeField]
	UnityEngine.Audio.AudioMixer mixer;

	public float masterVolume
	{
		set { mixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 20, value)); }
	}

	public float seVolume
	{
		set { mixer.SetFloat("SEVolume", Mathf.Lerp(-80, 20, value)); }
	}

	public float bgmVolume
	{
		set { mixer.SetFloat("BGMVolume", Mathf.Lerp(-80, 20, value)); }
	}
}
