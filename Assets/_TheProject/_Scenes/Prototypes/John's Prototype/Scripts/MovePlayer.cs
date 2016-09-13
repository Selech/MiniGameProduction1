using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovePlayer : MonoBehaviour {

	public Transform destination;
	public static GameObject levels;
	float rotationSpeed = 1.5f;

	// Use this for initialization
	void Start () {
		destination = GameObject.Find ("FinalDestination").transform;

		foreach (var obj in GameObject.FindGameObjectsWithTag ("CarriableDragNDrop")) {
			obj.GetComponent<BoxCollider> ().enabled = false;	
		}

	}
	
	// Update is called once per frame
	void Update () {

		StartCoroutine (rotateBike());

		if(Vector3.Distance (transform.position, destination.position) == 0.0){
			levels = GameObject.Find("Canvas").transform.FindChild("Levels").gameObject;
			if (levels != null) {
				levels.SetActive (true);
			}
		}
	}

	IEnumerator rotateBike() {
		if (transform.position != destination.position) {
			Vector3 dir = destination.position - transform.position;
			Quaternion rot = Quaternion.LookRotation (-dir);

			transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * rotationSpeed);			
			transform.position = Vector3.MoveTowards (transform.position, destination.position, Time.deltaTime * 1.05f);
		}
		yield return null;
	}
}
