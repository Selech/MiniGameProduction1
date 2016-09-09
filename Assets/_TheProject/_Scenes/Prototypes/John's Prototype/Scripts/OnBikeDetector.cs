using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnBikeDetector : MonoBehaviour {

	public GameObject boxCollider;
	//List<GameObject> GOList = new List<GameObject>();
	ArrayList GOList = new ArrayList();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other){
			Debug.Log ("hi");
			if (!GOList.Contains (other)) {
				GOList.Add (other.gameObject);
				Debug.Log ("add object");
			}
			foreach (var o in GOList) {
				Debug.Log ((o as GameObject).name);
			}	
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Box collider") {
			Debug.Log ("bye");
			if (GOList.Contains (other)) {
				GOList.Remove (other);
			}
			foreach (GameObject o in GOList) {
				Debug.Log (o.name);
			}	
		}
	}
}
