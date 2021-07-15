using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//컴포넌트
	private Collider col;

	//초기화
	void Start () {
		col = GetComponent<Collider>();
	}
	
	//업데이트
	void Update () {
		
	}

	//Boundary에 닿으면 충돌X
	private void OnCollisionStay(Collision collision)
	{
		if (collision.collider.CompareTag("Boundary"))
			col.isTrigger = true;
	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.CompareTag("Boundary"))
			col.isTrigger = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Boundary"))
			col.isTrigger = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Boundary"))
			col.isTrigger = false;
	}
}
