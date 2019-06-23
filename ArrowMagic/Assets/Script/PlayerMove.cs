using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour // プレイヤーの移動を管理するスクリプト
{
	public GameObject PlayerMaterial;
	public GameObject Cat_Player;
	public GameObject UpArrowMaterial, LeftArrowMaterial, RightArrowMaterial, DownArrowMaterial;
	public GameObject prefab, UpdateMaterial;
	public GameObject RightCol, LeftCol, UpCol, DownCol;
	public GameObject EndCard;
	public Animator Cat_Player_animator;
	public Vector3 MOVEX = new Vector3(1.0f, 0, 0); // x軸方向に１マス移動するときの距離
	public Vector3 MOVEZ = new Vector3(0, 0, 1.0f); // y軸方向に１マス移動するときの距離

	public float step = 0f;     // 移動速度

	private Texture MainTexture, StartTexture;
	private Texture UpTexture, LeftTexture, RightTexture, DownTexture;
	private Texture BeforeTexture, ObjBeforeTexture;
	private Material PlayerColor;
	bool animOne = true;
	bool idleOne = true;

	bool Right, Left, Up, Down;
	bool one;

	ObjectMove objectMove;
	NavMeshAgent agent;

	// 値の初期化メソッド
	void valueInit()
	{
		one = true;
	}

	void setInit()
	{
		EndCard.SetActive(false);
	}

	// コライダーの初期化メソッド
	void colInit()
	{
		RightCol.SetActive(false);
		LeftCol.SetActive(false);
		UpCol.SetActive(false);
		DownCol.SetActive(false);
	}

	// テクスチャの初期化メソッド
	void texInit()
	{
		PlayerColor = PlayerMaterial.GetComponent<Renderer>().material;
		StartTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		UpTexture = UpArrowMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		LeftTexture = LeftArrowMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		RightTexture = RightArrowMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		DownTexture = DownArrowMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");
		BeforeTexture = StartTexture;
	}

	void Start()
	{
		valueInit();
		colInit();
		texInit();
		agent = GetComponent<NavMeshAgent>();
		if (SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage2") objectMove = GameObject.FindGameObjectWithTag("Object").GetComponent<ObjectMove>();
	}

	void Update()
	{
		MainTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");

		if (SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage2")
		{
			if (objectMove.ObjStartTexture != objectMove.ObjMainTexture)
			{
				ObjBeforeTexture = StartTexture;
				NoContinuityArrow();
			}
		}

		SetUpdateTexture();
		AnimationMigration();
	}

	// 入れた矢印の向きに応じて「コライダーを表示」、「矢印のテクスチャを取得」、「移動後の位置を取得」するメソッド
	void SetUpdateTexture()
	{
		if (MainTexture == RightTexture && !Right)
		{
			ColSwitchOff();
			RightCol.SetActive(true);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}

		if (MainTexture == LeftTexture && !Left)
		{
			ColSwitchOff();
			LeftCol.SetActive(true);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}

		if (MainTexture == UpTexture && !Up)
		{
			ColSwitchOff();
			UpCol.SetActive(true);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}

		if (MainTexture == DownTexture && !Down)
		{
			ColSwitchOff();
			DownCol.SetActive(true);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}
	}

	void SetTargetPosition()
	{
		if (MainTexture == RightTexture && !Right)
		{
			agent.Move(MOVEX * (Time.deltaTime * step));
		}

		if (MainTexture == LeftTexture && !Left)
		{
			agent.Move(-MOVEX * (Time.deltaTime * step));
		}

		if (MainTexture == UpTexture && !Up)
		{
			agent.Move(MOVEZ * (Time.deltaTime * step));
		}

		if (MainTexture == DownTexture && !Down)
		{
			agent.Move(-MOVEZ * (Time.deltaTime * step));
		}
	}

	// ゴールの判定を取るメソッド
	void OnTriggerEnter(Collider goal)
	{
		if (goal.gameObject.tag == "Goal")
		{
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
			if (one)
			{
				SoundManager.StopSE(6);
				SoundManager.PlaySE(4);
				one = false;
				EndCard.SetActive(true);
			}
		}
	}

	// 障害物に当たったら止まるメソッド
	public void RelayOnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			BeforeTexture = MainTexture;
			ObjBeforeTexture = StartTexture;
			NoContinuityArrow();
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
			idleOne = true;
		}
		else if (collider.gameObject.tag == "Object")
		{
			ObjBeforeTexture = MainTexture;
			BeforeTexture = StartTexture;
			NoContinuityArrow();
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
			idleOne = true;
		}
	}

	public void RelayOnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
			idleOne = true;
		}
		else if (collider.gameObject.tag == "Object")
		{
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
			idleOne = true;
		}
	}

	// 連続で同じ方向の矢印が入れられないようにするメソッド
	public void NoContinuityArrow()
	{
		if (BeforeTexture == RightTexture || ObjBeforeTexture == RightTexture)
		{
			ArrowSwitchOff();
			Right = true;
			return;
		}
		else if (BeforeTexture == LeftTexture || ObjBeforeTexture == LeftTexture)
		{
			ArrowSwitchOff();
			Left = true;
			return;
		}
		else if (BeforeTexture == UpTexture || ObjBeforeTexture == UpTexture)
		{
			ArrowSwitchOff();
			Up = true;
			return;
		}
		else if (BeforeTexture == DownTexture || ObjBeforeTexture == DownTexture)
		{
			ArrowSwitchOff();
			Down = true;
			return;
		}
		else
		{
			ArrowSwitchOff();
			return;
		}
	}

	void AnimationMigration()
	{
		if (MainTexture == StartTexture)
		{
			if (idleOne)
			{
				Cat_Player_animator.SetTrigger("isIdle");
				animOne = true;
				idleOne = false;
			}
		}

		if (MainTexture == UpTexture)
		{
			if (animOne)
			{
				Cat_Player_animator.SetTrigger("isTurn");
				Cat_Player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				Cat_Player_animator.SetTrigger("isWalk");
				animOne = false;
			}
		}

		if (MainTexture == DownTexture)
		{
			if (animOne)
			{
				Cat_Player_animator.SetTrigger("isTurn");
				Cat_Player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				Cat_Player_animator.SetTrigger("isWalk");
				animOne = false;
			}
		}

		if (MainTexture == RightTexture)
		{
			if (animOne)
			{
				Cat_Player_animator.SetTrigger("isTurn");
				Cat_Player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
				Cat_Player_animator.SetTrigger("isWalk");
				animOne = false;
			}
		}

		if (MainTexture == LeftTexture)
		{
			if (animOne)
			{
				Cat_Player_animator.SetTrigger("isTurn");
				Cat_Player.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
				Cat_Player_animator.SetTrigger("isWalk");
				animOne = false;
			}
		}

		if (Cat_Player_animator.GetCurrentAnimatorStateInfo(0).IsName("Cat_Player_Walking"))
		{
			SetTargetPosition();
		}
	}

	void ColSwitchOff()
	{
		DownCol.SetActive(false);
		RightCol.SetActive(false);
		LeftCol.SetActive(false);
		UpCol.SetActive(false);
	}

	void ArrowSwitchOff()
	{
		Right = false;
		Left = false;
		Up = false;
		Down = false;
	}

	public Texture PlStartTexture
	{
		get { return this.StartTexture; }
		private set { this.StartTexture = value; }
	}

	public Texture PlMainTexture
	{
		get { return this.MainTexture; }
		private set { this.MainTexture = value; }
	}
}
