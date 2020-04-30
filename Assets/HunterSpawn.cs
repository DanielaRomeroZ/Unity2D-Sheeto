using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpawn : MonoBehaviour {

	public GameObject hunter;

	// Use this for initialization
	void Start () {
		Invoke ("CallHunter", 3f);
	}
	
	// Update is called once per frame
	void CallHunter () {
		Instantiate (hunter);
		hunter.SetActive (true);
	}
}
