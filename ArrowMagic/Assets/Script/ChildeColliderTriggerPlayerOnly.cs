using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildeColliderTriggerPlayerOnly : MonoBehaviour // プレイヤーのUp,Down,Right,Leftコライダーの当たり判定をPlayerMoveスクリプトに送るスクリプト
{
	PlayerMove playerMove;
	MouseDragPlayerOnly mouseDragPlayerOnly;

	public GameObject ArrowCube;

	// Use this for initialization
	void Start()
	{
		GameObject objColliderTriggerParent = gameObject.transform.parent.gameObject;
		playerMove = objColliderTriggerParent.GetComponent<PlayerMove>();
		mouseDragPlayerOnly = ArrowCube.GetComponent<MouseDragPlayerOnly>();
	}

	void OnTriggerEnter(Collider collider)
	{
		playerMove.RelayOnTriggerEnter(collider);
	}

	void OnTriggerStay(Collider collider)
	{
		mouseDragPlayerOnly.DragOnTriggerStay(collider);
		playerMove.RelayOnTriggerStay(collider);
	}
}
