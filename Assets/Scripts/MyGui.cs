using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGui : MonoBehaviour {

	// Use this for initialization
	public void MyScene_Start() {

		SceneManager.LoadScene(2);

	}

	public void Exit() {

		Application.Quit ();

	}

}
