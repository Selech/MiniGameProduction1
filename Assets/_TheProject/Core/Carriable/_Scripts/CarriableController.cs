using UnityEngine;
using System.Collections;

public class CarriableController : MonoBehaviour {
	FixedJoint joint;
	GameObject connectedBody;

	public float breakForce = 40;

	void Start(){
		joint = GetComponent<FixedJoint> ();
		connectedBody = joint.connectedBody.gameObject;
	}

	void OnJointBreak(){
		connectedBody.GetComponent<FixedJoint> ().breakForce = breakForce;
	}
}
