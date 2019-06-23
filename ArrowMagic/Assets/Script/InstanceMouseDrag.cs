using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceMouseDrag : MonoBehaviour
{
	bool isInside = false;

	void Update()
	{
		Check();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			isInside = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			isInside = false;
		}
	}

	void Check()
	{
		if (isInside && Input.GetMouseButton(0) == false)
		{

		}

		if (!isInside && Input.GetMouseButton(0) == false)
		{
			Destroy(gameObject);
		}
	}

	public void OnDrag()
	{
		Vector3 TapPos = Input.mousePosition;
		TapPos.z = 6f;
		transform.position = Camera.main.ScreenToWorldPoint(TapPos);
	}
}
