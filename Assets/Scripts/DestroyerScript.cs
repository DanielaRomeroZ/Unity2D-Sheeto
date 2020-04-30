using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {

	public int restartScene = 2;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			AudioSource audio = GetComponent<AudioSource> ();
			Camera.main.GetComponent<AudioSource> ().Stop ();
			audio.Play ();
			Invoke ("Restart", 3f);
			return;
		}

		if (col.gameObject.transform.parent) {
			Destroy (col.gameObject.transform.parent.gameObject);
		} else {
			Destroy (col.gameObject);
		}
	}

	void Restart(){
		SceneManager.LoadScene (1);
	}
		
}
