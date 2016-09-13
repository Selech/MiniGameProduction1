using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroSceneManager : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	int carriablesAmount = 0;
	public bool swipe = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Swipe ();
	}

	public void OpenScene(string scene){
		SceneManager.LoadScene (scene);
	}

	public void Swipe()
	{
		carriablesAmount = GameObject.FindObjectOfType<OnBikeDetector> ().CollectedCarriables.Count;
		if(Input.touches.Length > 0 && carriablesAmount >= 1 && carriablesAmount <= 6)
		{
			Touch t = Input.GetTouch(0);
			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			}
			if (t.phase == TouchPhase.Ended) {
				secondPressPos = new Vector2 (t.position.x, t.position.y);
				float distance = Vector2.Distance (firstPressPos, secondPressPos);
				Ray ray1 = Camera.main.ScreenPointToRay (firstPressPos);
				Ray ray2 = Camera.main.ScreenPointToRay (secondPressPos);
				RaycastHit hit1, hit2;
				if (distance > 0 && Physics.Raycast(ray1, out hit1) && hit1.transform.gameObject.tag == "Basket" && Physics.Raycast(ray2, out hit2) && hit2.transform.gameObject.tag == "Basket") {
					GameObject camera = GameObject.Find ("Main Camera");
					camera.gameObject.AddComponent<CamMove> ();
					GameObject basket = GameObject.FindGameObjectWithTag ("Player");
					basket.AddComponent<MovePlayer> ();
					camera.transform.parent = basket.transform;
					foreach (GameObject carriable in GameObject.FindObjectOfType<OnBikeDetector> ().CollectedCarriables) {
						carriable.transform.parent = basket.transform;
					}
				}
			}
		}
		if (swipe && carriablesAmount >= 1 && carriablesAmount <= 6) {
			GameObject camera = GameObject.Find ("Main Camera");
			camera.gameObject.AddComponent<CamMove> ();
			GameObject basket = GameObject.FindGameObjectWithTag ("Player");
			basket.AddComponent<MovePlayer> ();
			camera.transform.parent = basket.transform;
			foreach (GameObject carriable in GameObject.FindObjectOfType<OnBikeDetector> ().CollectedCarriables) {
				carriable.transform.parent = basket.transform;
			}
		}
	}
}
