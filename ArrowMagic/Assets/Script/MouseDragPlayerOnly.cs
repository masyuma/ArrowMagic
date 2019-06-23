using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseDragPlayerOnly : MonoBehaviour //矢印をドラッグ移動、個数の増減、矢印を適用させるかどうかを管理するスクリプト
{
	public GameObject PlayerMaterial;
	public GameObject CubeMaterial;
	public GameObject ArrowCol;
	public GameObject SuccessParticle, FailureParticle;
	public Text TargetText;
	public int CubeCount;
	public int TapPosPer;
	private GameObject Player;
	private Vector3 startPosition;
	private Texture MainTexture, StartTexture;
	private Texture MainTexture1, StartTexture1;
	bool isInside = false;
	bool isObstacle = false;

	StageClearButtonScript stageClearButtonScript;

	void Start()
	{
		StartTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		startPosition = this.transform.position;
		TargetText.text = "×" + CubeCount;
		isObstacle = false;
		Player = GameObject.Find("PlayerDrag");
		stageClearButtonScript = GameObject.Find("GameManager").GetComponent<StageClearButtonScript>();
	}

	void Update()
	{
		MainTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		ActiveSwitch();
	}

	// 矢印をドラッグで移動するためのメソッド
	public void OnDrag()
	{
		Vector3 TapPos = Input.mousePosition;
		TapPos.z = TapPosPer;
		transform.position = Camera.main.ScreenToWorldPoint(TapPos);
		Player.GetComponent<BoxCollider>().enabled = true;
	}

	// 矢印がプレイヤーに当たったかどうか判定するメソッド
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerDrag")
		{
			isInside = true;
		}
	}

	// 矢印がオブジェクトに当たったかどうか判定するメソッド
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "PlayerDrag")
		{
			isInside = false;
		}
	}

	// 矢印が障害物に当たったかどうか判定するメソッド
	public void DragOnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			isObstacle = true;
		}
	}

	// 矢印のコライダーが消えているか判定するメソッド
	void ActiveSwitch()
	{
		if (!ArrowCol.activeSelf)
		{
			isObstacle = false;
		}
	}
	// 矢印の処理をするメソッド
	public void Check()
	{
		Player.GetComponent<BoxCollider>().enabled = false;
		if (isInside)
		{
			if (CubeCount != 0 && MainTexture == StartTexture)
			{
				SoundManager.PlaySE(1);
				PlayerMaterial.GetComponent<Renderer>().material = CubeMaterial.GetComponent<Renderer>().material;
				if (!isObstacle)
				{
					CubeCount--;
					GameObject SP = Instantiate(SuccessParticle, transform.position, transform.rotation) as GameObject;
					Destroy(SP, 2f);
				}
				else
				{
					GameObject FP = Instantiate(FailureParticle, transform.position, transform.rotation) as GameObject;
					Destroy(FP, 2f);
				}
				transform.position = startPosition;
				TargetText.text = "×" + CubeCount;
				isInside = false;
				Debug.Log("PlayerHit");
			}
			else
			{
				GameObject FP = Instantiate(FailureParticle, transform.position, transform.rotation) as GameObject;
				Destroy(FP, 2f);
				SoundManager.PlaySE(2);
				transform.position = startPosition;
				Debug.Log("PlayerOtherHit");
			}
		}
		else
		{
			GameObject FP = Instantiate(FailureParticle, transform.position, transform.rotation) as GameObject;
			Destroy(FP, 2f);
			SoundManager.PlaySE(2);
			transform.position = startPosition;
			Debug.Log("NotHit");
		}
	}
}