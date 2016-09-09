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

	[Range(0.01f,0.3f)]
	public float deadZone = 0.1f;

	public Rigidbody body;

	void Start(){
		AkSoundEngine.PostEvent ("Play_Pedal",this.gameObject);
		AkSoundEngine.PostEvent ("Play_Ambience",this.gameObject);
	}

	void OnEnable(){
		EventManager.StartListening (GameManager.Instance._eventsContainer.obstacleHit, Jump);
	}

	void OnDisable(){
		AkSoundEngine.PostEvent ("Stop_Pedal",this.gameObject);
		EventManager.StopListening (GameManager.Instance._eventsContainer.obstacleHit, Jump);
	}

	void FixedUpdate(){
		Move ();
	}

	void Move()
	{
		//Debug.Log (Input.acceleration.x);
		float sideSpeed = Mathf.Abs (Input.acceleration.x) > deadZone ? Input.acceleration.x : 0;
		var target = Quaternion.Euler (0, 0, -sideSpeed * rotationMultiplier);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime*rotationSpeed);

		if (transform.rotation.eulerAngles.z > 180) {
			rotationAngle = 360 - transform.rotation.eulerAngles.z;
		} else {
			rotationAngle = -transform.rotation.eulerAngles.z;
		}

		rotationAngle /= 500;

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + rotationAngle, transform.position.y, transform.position.z), 1/strafeReduction);
		body.AddForce (new Vector3(0f, 0f, forwardSpeed), ForceMode.VelocityChange);
//		transform.Translate(new Vector3(0f, 0f, forwardSpeed));
	}

	void Jump(){
		AkSoundEngine.PostEvent ("Play_Collision", this.gameObject);
		body.AddForce (new Vector3(0,GameManager.Instance.obstacleForceAddUp,0), ForceMode.VelocityChange);
	}
}
