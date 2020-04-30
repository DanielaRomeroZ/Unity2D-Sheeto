using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacksaw_Behaviour : MonoBehaviour {

	public float rotVelocity = 400f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,rotVelocity*Time.deltaTime);
	}
}
