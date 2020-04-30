﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadTargetSceneButton : MonoBehaviour {

	// Use this for initialization
	public void LoadSceneNum( int num) {
		if (num < 0 || num >= SceneManager.sceneCountInBuildSettings) {
			Debug.LogWarning ("Can't load scene num " + num + ", SceneManager only has " + SceneManager.sceneCountInBuildSettings + " scenes in BuildSettings!");
			return;

		}

		LoadingScreenManager.LoadScene(num);
	}
}﻿