using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Range(0.0f, 60.0f)]
	public float rotationMultiplier = 0.1f;

	[Range(5.0f, 15.0f)]
	public float rotationSpeed = 10f;

	[Range(10.0f, 30.0f)]
	public float strafeReduction = 20f;

	private float rotationAngle = 0f;

	[Range(0.01f, 0.1f)]
	public float forwardSpeed = 0.05f;

	void FixedUpdate(){
		Debug.Log (Input.acceleration.x);
		var target = Quaternion.Euler (0, 0, -Input.acceleration.x * rotationMultiplier);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime*rotationSpeed);

		if (transform.rotation.eulerAngles.z > 180) {
			rotationAngle = 360 - transform.rotation.eulerAngles.z;
		} else {
			rotationAngle = -transform.rotation.eulerAngles.z;
		}

		rotationAngle /= 500;

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + rotationAngle, transform.position.y, transform.position.z), 1/strafeReduction);
		Debug.Log (rotationAngle);
		transform.Translate(new Vector3(0f, 0f, forwardSpeed));

	}
}
