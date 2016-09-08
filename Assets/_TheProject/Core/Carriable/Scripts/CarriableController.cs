using UnityEngine;
using System.Collections;

public class CarriableController : MonoBehaviour {

	private Rigidbody rb;
	private int speed = 50;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 moveDir = Vector3.zero;
		moveDir.x = Input.GetAxis ("Horizontal");
		moveDir.y = Input.GetAxis ("Vertical");
		transform.position += moveDir * speed * Time.deltaTime;
	}

	void OnJointBreak(float breakForce){
		Debug.Log (breakForce);
	}
//
//	void OnCollisionEnter(Collision collision) {
//		GameObject c = collision.gameObject;
//		Debug.Log (c.name);
//		if (c.name.Contains("Carriable")) {
//			var joint = gameObject.AddComponent<FixedJoint> ();
//			joint.connectedBody = c.GetComponent<Rigidbody>();
//		}
//	}
}
