using UnityEngine;
using System.Collections;

public class CarriableController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		GameObject c = collision.gameObject;
		Debug.Log (c.name);
		if (c.name.Contains("Carriable")) {
			var joint = gameObject.AddComponent<FixedJoint> ();
			joint.connectedBody = c.GetComponent<Rigidbody>();
		}
	}
}
