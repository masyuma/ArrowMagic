using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour //サウンドを格納し、再生するスクリプト
{

	public AudioSource[] audioSource;

	private static SoundManager thisObj;

	private static float playTime;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		thisObj = this; //このスクリプトのついたオブジェクトを取得
	}

	public static void PlaySE(int i)
	{
		//もしこのオブジェクトが空じゃなかったら i番目に格納したサウンドを再生する
		if (thisObj != null)
		{
			thisObj.audioSource[i].time = 0f;
			thisObj.audioSource[i].Play();
		}
	}

	public static void StopSE(int i)
	{
		//もしこのオブジェクトが空じゃなかったら i番目に格納したサウンドを再生する
		if (thisObj != null)
		{
			thisObj.audioSource[i].Stop();
		}
	}

	public static void TimeSE(int i)
	{
		//もしこのオブジェクトが空じゃなかったら i番目に格納したサウンドを再生する
		if (thisObj != null)
		{
			playTime = thisObj.audioSource[i].time;
		}
	}

	public static void EnrouteSE(int i)
	{
		//もしこのオブジェクトが空じゃなかったら i番目に格納したサウンドを再生する
		if (thisObj != null)
		{
			thisObj.audioSource[i].time = playTime;
			thisObj.audioSource[i].Play();
		}
	}

	public static void ResetSE()
	{
		//もしこのオブジェクトが空じゃなかったら i番目に格納したサウンドを再生する
		if (thisObj != null)
		{
			playTime = 0;
		}
	}
}