using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LanguageSelectionManager : MonoBehaviour {

	public RectTransform highlight;
	Image[] childrenImage;

	public bool languageSelected;

	public Animator animator;


	void Start() {
		childrenImage = this.GetComponentsInChildren<Image> ();
	}

	public void SelectEnglish(){
		CarriableManager.Instance.isEnglish = true;
		languageSelected = false;
		GameManager.Instance.PlayUIClick();
		highlight.GetComponent<Image> ().enabled = true;
		highlight.localPosition = new Vector3 (-180,0,0);

		foreach (var img in childrenImage) {
			img.CrossFadeAlpha (0f, 1f, true);
		}

		StartCoroutine (disableCanvas ());
	}

	public void SelectDanish(){
		GameManager.Instance.PlayUIClick();
		CarriableManager.Instance.isEnglish = false;
		languageSelected = true;
		highlight.GetComponent<Image> ().enabled = true;
		highlight.localPosition = new Vector3 (160,0,0);
	
		foreach (var img in childrenImage) {
			img.CrossFadeAlpha (0f, 1f, true);
		}

		StartCoroutine (disableCanvas ());
	}

	IEnumerator disableCanvas() {
		this.gameObject.SetActive (false);
		animator.SetTrigger ("Intro");

		if(CarriableManager.Instance.isEnglish)
			GameManager.Instance.PlaySound ("Play_VO_Intro_EN");
		else
			GameManager.Instance.PlaySound ("Play_VO_Intro_DA");

		yield return null;
	}

	public void Back(){
		SceneManager.LoadScene ("t1.1");
	}
}

