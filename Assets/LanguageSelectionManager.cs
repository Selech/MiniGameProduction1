using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LanguageSelectionManager : MonoBehaviour {

	public RectTransform highlight;

	public void SelectEnglish(){
		highlight.localPosition = new Vector3 (-200,0,0);
	}

	public void SelectDanish(){
		highlight.localPosition = new Vector3 (200,0,0);
	}

	public void Back(){
		SceneManager.LoadScene ("t1.1");
	}
}

