using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

	float distance = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition =  Camera.main.ScreenToWorldPoint (mousePosition);

		transform.position = objPosition;
	}
}
