using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCam : MonoBehaviour {

	public GameObject target;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () {
		float posX = target.transform.position.x+0.8f;
		float posY = target.transform.position.y+1.2f;

		transform.position = new Vector3 (posX, posY, transform.position.z);
	}
}
