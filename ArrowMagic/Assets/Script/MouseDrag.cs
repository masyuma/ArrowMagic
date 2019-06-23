using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseDrag : MonoBehaviour //矢印をドラッグ移動、個数の増減、矢印を適用させるかどうかを管理するスクリプト
{
	public GameObject PlayerMaterial;
	public GameObject ObjectMaterial;
	public GameObject CubeMaterial;
	public GameObject ArrowCol;
	public GameObject ObjArrowCol;
	public GameObject SuccessParticle, FailureParticle;
	public Text TargetText;
	public int CubeCount;
	public int TapPosPer;
	private GameObject Player;
	private GameObject Object;
	private Vector3 startPosition;
	private Texture MainTexture, StartTexture;
	private Texture MainTexture1, StartTexture1;
	bool isInside = false;
	bool isInsideObj = false;
	bool isObstacle = false;
	bool isObstacleObj = false;

	StageClearButtonScript stageClearButtonScript;

	void Start()
	{
		StartTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		StartTexture1 = ObjectMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		startPosition = this.transform.position;
		TargetText.text = "×" + CubeCount;
		isObstacle = false;
		isObstacleObj = false;
		Player = GameObject.Find("PlayerDrag");
		Object = GameObject.Find("ObjectDrag");
		stageClearButtonScript = GameObject.Find("GameManager").GetComponent<StageClearButtonScript>();
	}

	void Update()
	{
		MainTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		MainTexture1 = ObjectMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		ActiveSwitch();
		ActiveSwitchObj();
	}

	// 矢印をドラッグで移動するためのメソッド
	public void OnDrag()
	{
		Vector3 TapPos = Input.mousePosition;
		TapPos.z = TapPosPer/*(4 * TapPos.y / Screen.height) * Mathf.Sin(90.0f) + (TapPosPer/2f)*/; 
		transform.position = Camera.main.ScreenToWorldPoint(TapPos);
		Player.GetComponent<BoxCollider>().enabled = true;
		if(Object != null)Object.GetComponent<BoxCollider>().enabled = true;
	}

	// 矢印がプレイヤーに当たったかどうか判定するメソッド
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerDrag")
		{
			isInside = true;
		}

		if (other.gameObject.tag == "ObjectDrag")
		{
			isInsideObj = true;
		}
	}

	// 矢印がオブジェクトに当たったかどうか判定するメソッド
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "PlayerDrag")
		{
			isInside = false;
		}

		if (other.gameObject.tag == "ObjectDrag")
		{
			isInsideObj = false;
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

	public void ObjDragOnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			isObstacleObj = true;
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

	void ActiveSwitchObj()
	{
		if (!ObjArrowCol.activeSelf)
		{
			isObstacleObj = false;
		}
	}

	// 矢印の処理をするメソッド
	public void Check()
	{
		Player.GetComponent<BoxCollider>().enabled = false;
		if (Object != null) Object.GetComponent<BoxCollider>().enabled = false;
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
        else if (isInsideObj)
		{
			if (CubeCount != 0 && MainTexture1 == StartTexture1)
			{
				SoundManager.PlaySE(3);
				ObjectMaterial.GetComponent<Renderer>().material = CubeMaterial.GetComponent<Renderer>().material;
				if (!isObstacleObj)
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
				Debug.Log("ObjectHit");
			}
			else
			{
				GameObject FP = Instantiate(FailureParticle, transform.position, transform.rotation) as GameObject;
				Destroy(FP, 2f);
				SoundManager.PlaySE(2);
				transform.position = startPosition;
				Debug.Log("ObjectOtherHit");
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