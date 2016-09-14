using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnBikeDetector : MonoBehaviour {

	public List<GameObject> CollectedCarriables = new List<GameObject>();
	public GameObject CurrentCarriable;
	public float currentHeight = 0.0f;

	private Vector3 target;
	private float extraHeight = 0.3f;
	private float extraDepth = 0.15f;
	public bool stackingDone = false;

	public void addObject(GameObject go, float height) {
		CurrentCarriable = go;

		if (CollectedCarriables.Count < 4) { 
			if (!CollectedCarriables.Contains (go)) {
				CollectedCarriables.Add (go);
				currentHeight += height;

				target = new Vector3 (-0.01047623f,2.315246f + (currentHeight * extraHeight), -4.2356f - (currentHeight * extraDepth));
			} else {
				sortObjects ();
			}
			CarriableManager.Instance.convertToStringArray (CollectedCarriables);
		}
	}

	void Start(){
		target = Camera.main.transform.position;
	}

	void Update(){
		if(!stackingDone){
			Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, target, 0.02f);
		}
	}

	public void removeObject(GameObject go, float height) {
		
		foreach (var v in CollectedCarriables) {
			if (go == v) {
				CollectedCarriables.Remove (v);
				currentHeight -= height;
				sortObjects ();	
				break;
			}
		}
		CarriableManager.Instance.convertToStringArray (CollectedCarriables);
		target = new Vector3 (-0.01047623f,2.315246f + (currentHeight * extraHeight), -4.2356f - (currentHeight * extraDepth));
		CurrentCarriable = null;
	}

	public void sortObjects() {
		currentHeight = 0.0f;
		foreach (var obj in CollectedCarriables) {
			obj.GetComponent<DragDrop> ().Sort ();
		}
	}
}
