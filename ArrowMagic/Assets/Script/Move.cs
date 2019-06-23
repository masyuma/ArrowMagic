using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
	[SerializeField] float speed = 1;

	NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		var move = new Vector3(Input.GetAxis("Horizontal"), 0f ,Input.GetAxis("Vertical"));

		if (move != Vector3.zero)
		{
			Vector3 direction = move;
			transform.localRotation = Quaternion.LookRotation(direction);

			// キャラクターの位置を移動させる
			agent.Move(direction * (Time.deltaTime * speed));
		}

	}
}