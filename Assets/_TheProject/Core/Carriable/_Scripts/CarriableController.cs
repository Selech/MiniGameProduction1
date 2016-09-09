using UnityEngine;
using System.Collections;

public class CarriableController : MonoBehaviour {
	HingeJoint joint;

	void Update(){
		joint = GetComponent<HingeJoint> ();
		Debug.Log (joint.angle);
	}
}
