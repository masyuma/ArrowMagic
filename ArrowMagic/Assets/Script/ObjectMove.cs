using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectMove : MonoBehaviour // オブジェクトの移動を管理するスクリプト
{
	public GameObject PlayerMaterial;
	public GameObject UpArrowMaterial, LeftArrowMaterial, RightArrowMaterial, DownArrowMaterial;
	public GameObject prefab, UpdateMaterial;
	public GameObject RightCol, LeftCol, UpCol, DownCol;
	public GameObject RotateModel;
	public Vector3 MOVEX = new Vector3(1.0f, 0, 0); // x軸方向に１マス移動するときの距離
	public Vector3 MOVEZ = new Vector3(0, 0, 1.0f); // y軸方向に１マス移動するときの距離

	public float step = 0f;     // 移動速度

	private Texture MainTexture, StartTexture;
	private Texture UpTexture, LeftTexture, RightTexture, DownTexture;
	private Texture BeforeTexture, PlBeforeTexture;
	private Material PlayerColor;

	bool Right, Left, Up, Down;

	PlayerMove playerMove;
	NavMeshAgent agent;

	//コライダーの初期化メソッド
	void colInit()
	{
		RightCol.SetActive(false);
		LeftCol.SetActive(false);
		UpCol.SetActive(false);
		DownCol.SetActive(false);
	}

	//テクスチャの初期化メソッド
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
		colInit();
		texInit();
		agent = GetComponent<NavMeshAgent>();
		playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		MainTexture = PlayerMaterial.GetComponent<Renderer>().material.GetTexture("_MainTex");

		if (playerMove.PlStartTexture != playerMove.PlMainTexture)
		{
			PlBeforeTexture = StartTexture;
			NoContinuityArrow();
		}

		SetUpdateTexture();
		SetTargetPosition();
		Move();
	}

	void SetUpdateTexture()
	{
		if (MainTexture == RightTexture && !Right)
		{
			ColSwitchOff();
			RightCol.SetActive(true);
			RotateModel.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}

		if (MainTexture == LeftTexture && !Left)
		{
			ColSwitchOff();
			LeftCol.SetActive(true);
			RotateModel.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}

		if (MainTexture == UpTexture && !Up)
		{
			ColSwitchOff();
			UpCol.SetActive(true);
			RotateModel.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}

		if (MainTexture == DownTexture && !Down)
		{
			ColSwitchOff();
			DownCol.SetActive(true);
			RotateModel.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
			UpdateMaterial.GetComponent<Renderer>().material = PlayerMaterial.GetComponent<Renderer>().material;
			return;
		}
	}

	// 入れた矢印の向きに応じて「コライダーを表示」、「矢印のテクスチャを取得」、「移動後の位置を取得」するメソッド
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

	// オブジェクトの移動メソッド
	void Move()
	{
		SetTargetPosition();
	}

	// 障害物に当たったら止まるメソッド
	public void RelayOnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			BeforeTexture = MainTexture;
			PlBeforeTexture = StartTexture;
			NoContinuityArrow();
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
		}
		else if (collider.gameObject.tag == "Player")
		{
			PlBeforeTexture = MainTexture;
			BeforeTexture = StartTexture;
			NoContinuityArrow();
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
		}
	}

	public void RelayOnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
		}
		else if (collider.gameObject.tag == "Player")
		{
			PlayerMaterial.GetComponent<Renderer>().material = PlayerColor;
		}
	}

	// 連続で同じ方向の矢印が入れられないようにするメソッド
	void NoContinuityArrow()
	{
		if (BeforeTexture == RightTexture || PlBeforeTexture == RightTexture)
		{
			ArrowSwitchOff();
			Right = true;
			return;
		}
		else if (BeforeTexture == LeftTexture || PlBeforeTexture == LeftTexture)
		{
			ArrowSwitchOff();
			Left = true;
			return;
		}
		else if (BeforeTexture == UpTexture || PlBeforeTexture == UpTexture)
		{
			ArrowSwitchOff();
			Up = true;
			return;
		}
		else if (BeforeTexture == DownTexture || PlBeforeTexture == DownTexture)
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

	public Texture ObjStartTexture
	{
		get { return this.StartTexture; }
		private set { this.StartTexture = value; }
	}

	public Texture ObjMainTexture
	{
		get { return this.MainTexture; }
		private set { this.MainTexture = value; }
	}
}

