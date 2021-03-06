﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public void Start() {
		Application.targetFrameRate = 60;
	}

	public void LoadScene (string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void LoadThisScene () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
