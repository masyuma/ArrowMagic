using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildeColliderTrigger : MonoBehaviour // プレイヤーのUp,Down,Right,Leftコライダーの当たり判定をPlayerMoveスクリプトに送るスクリプト
{
	PlayerMove playerMove;
	MouseDrag mouseDrag;

	public GameObject ArrowCube;

	// Use this for initialization
	void Start()
	{
		GameObject objColliderTriggerParent = gameObject.transform.parent.gameObject;
		playerMove = objColliderTriggerParent.GetComponent<PlayerMove>();
		mouseDrag = ArrowCube.GetComponent<MouseDrag>();
	}

	void OnTriggerEnter(Collider collider)
	{
		playerMove.RelayOnTriggerEnter(collider);
	}

	void OnTriggerStay(Collider collider)
	{
		mouseDrag.DragOnTriggerStay(collider);
		playerMove.RelayOnTriggerStay(collider);
	}
}
