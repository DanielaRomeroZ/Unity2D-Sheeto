using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartLvL : MonoBehaviour {

	// Use this for initialization
	public void Yep () {
		SceneManager.LoadScene (2);
	}
	
	// Update is called once per frame
	public void Nope () {
		SceneManager.LoadScene (0);
	}
}
