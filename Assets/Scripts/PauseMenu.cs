using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour {

	private bool paused;

	public GameObject panelpausa;
	public GameObject panelgui;

	void Start () {
		panelpausa.SetActive (false);
	}

	public void Pause () {
		Debug.Log (paused);
		paused = !paused;
		if (paused) {
			Time.timeScale = 0;
			//TODO set a proper way of pausing this w/out using timescale
			panelpausa.SetActive (true);
			panelgui.SetActive (false);
		} else {
			Time.timeScale = 1;
			panelpausa.SetActive (false);
			panelgui.SetActive (true);
		}
	}

	public void MainMenu (){
		Pause ();
		SceneManager.LoadScene (0);
	}

}
