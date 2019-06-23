using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildeColliderTrigger2 : MonoBehaviour // オブジェクトのUp,Down,Right,Leftコライダーの当たり判定をObjectMoveスクリプトに送るスクリプト
{
	ObjectMove objectMove;
	MouseDrag mouseDrag;

	public GameObject ArrowCube;

	// Use this for initialization
	void Start()
	{
		GameObject objColliderTriggerParent = gameObject.transform.parent.gameObject;
		objectMove = objColliderTriggerParent.GetComponent<ObjectMove>();
		mouseDrag = ArrowCube.GetComponent<MouseDrag>();
	}

	void OnTriggerEnter(Collider collider)
	{
		objectMove.RelayOnTriggerEnter(collider);
	}

	void OnTriggerStay(Collider collider)
	{
		mouseDrag.ObjDragOnTriggerStay(collider);
		objectMove.RelayOnTriggerStay(collider);
	}
}
