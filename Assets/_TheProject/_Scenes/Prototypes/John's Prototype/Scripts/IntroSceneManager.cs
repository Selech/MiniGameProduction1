using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroSceneManager : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	int carriablesAmount = 0;
	public bool startBike = false;
	public GameObject runButton;
	public LanguageSelectionManager languageScript;
	
	// Update is called once per frame
	void FixedUpdate () {
		Swipe ();
		if (startBike) {
			startBike = false;
			Swipe ();
		}

		if (carriablesAmount >= 1 && !startBike) {
			runButton.SetActive (true);
		} else {
			runButton.SetActive (false);
		}

	}

	public void OpenScene(int scene){
		GameManager.Instance.PlayUIClick();
		if (scene == 1 && !languageScript.GetComponent<LanguageSelectionManager>().languageSelected) {
			SceneManager.LoadScene (3);
		} else {
			SceneManager.LoadScene (scene);
		}
	}

	public void Swipe() {
		carriablesAmount = GameObject.FindObjectOfType<OnBikeDetector> ().CollectedCarriables.Count;

		if (Input.touches.Length > 0 && carriablesAmount >= 1 && carriablesAmount <= 4) {
			Touch t = Input.GetTouch (0);

			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			}

			if (t.phase == TouchPhase.Ended) {
				secondPressPos = new Vector2 (t.position.x, t.position.y);
				float distance = Vector2.Distance (firstPressPos, secondPressPos);
				Ray ray1 = Camera.main.ScreenPointToRay (firstPressPos);
				Ray ray2 = Camera.main.ScreenPointToRay (secondPressPos);
				RaycastHit hit1, hit2;

				if (distance > 0 && Physics.Raycast (ray1, out hit1) && hit1.transform.gameObject.tag == "Basket" && Physics.Raycast (ray2, out hit2) && hit2.transform.gameObject.tag == "Basket") {
					GameObject camera = GameObject.Find ("Main Camera");

					camera.gameObject.AddComponent<CamMove> ();
					GameObject basket = GameObject.FindGameObjectWithTag ("Player");

					basket.AddComponent<MovePlayer> ();
					camera.transform.parent = basket.transform;

					GameObject.Find ("OnBikeDetector").GetComponent<OnBikeDetector> ().stackingDone = true;

					foreach (GameObject carriable in GameObject.FindObjectOfType<OnBikeDetector> ().CollectedCarriables) {
						carriable.transform.parent = basket.transform;
					}

				}
			}
		}
	}

	public void goBike() {
		runButton.SetActive (false);
		startBike = true;
		GameObject camera = GameObject.Find ("Main Camera");

		camera.gameObject.AddComponent<CamMove> ();
		GameObject basket = GameObject.FindGameObjectWithTag ("Player");

		basket.AddComponent<MovePlayer> ();
		camera.transform.parent = basket.transform;

		GameObject.Find ("OnBikeDetector").GetComponent<OnBikeDetector> ().stackingDone = true;

		foreach (GameObject carriable in GameObject.FindObjectOfType<OnBikeDetector> ().CollectedCarriables) {
			carriable.transform.parent = basket.transform;
		}			
	}
}
