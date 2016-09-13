﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LanguageSelectionManager : MonoBehaviour {

	public RectTransform highlight;
	Image[] childrenImage;


	void Start() {
		childrenImage = this.GetComponentsInChildren<Image> ();
	}

	public void SelectEnglish(){
		highlight.GetComponent<Image> ().enabled = true;
		highlight.localPosition = new Vector3 (-180,0,0);

		foreach (var img in childrenImage) {
			img.CrossFadeAlpha (0f, 1f, true);
		}

		StartCoroutine (disableCanvas ());
	}

	public void SelectDanish(){
		highlight.GetComponent<Image> ().enabled = true;
		highlight.localPosition = new Vector3 (160,0,0);
	
		foreach (var img in childrenImage) {
			img.CrossFadeAlpha (0f, 1f, true);
		}

		StartCoroutine (disableCanvas ());
	}

	IEnumerator disableCanvas() {
		yield return new WaitForSeconds (1f);
		this.gameObject.SetActive (false);
	}

	public void Back(){
		SceneManager.LoadScene ("t1.1");
	}
}

