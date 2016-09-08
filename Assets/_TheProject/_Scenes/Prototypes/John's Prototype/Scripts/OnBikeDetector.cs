using UnityEngine;
using System.Collections;

public class OnBikeDetector : MonoBehaviour {

	public GameObject boxCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Box collider") {
			Debug.Log ("hi");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Box collider") {
			Debug.Log ("bye");
		}
	}
}
