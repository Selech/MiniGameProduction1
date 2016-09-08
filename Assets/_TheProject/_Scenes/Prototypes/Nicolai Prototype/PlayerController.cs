using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float tiltSpeed = 0.0f; 

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){
		Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, 0.0f);
		this.GetComponent<Rigidbody>().velocity = movement * tiltSpeed;
	}


	// Update is called once per frame
	void Update () {
	
	}
}
