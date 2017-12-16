﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	public GameObject cont;

	void Start() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			cont.SetActive (true);
		} else {
			cont.SetActive (false);
		}
	}

	public void LoadScene(string name) {
		StartCoroutine(LoadSceneAfterDelay(name, 0.1f));
	}

	IEnumerator LoadSceneAfterDelay(string name, float delay) {
		yield return new WaitForSeconds(0.3f);
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
	}

	public void Quit() {
		Application.Quit ();
	}

    public void NewGame(string name)
    {
		File.Delete (Application.persistentDataPath + "/playerInfo.dat");
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
