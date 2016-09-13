using UnityEngine;
using System.Collections;

public class PlayerControllerv2 : MonoBehaviour
{

	[Range (0.0f, 60.0f)]
	public float rotationMultiplier = 0.1f;

	[Range (5.0f, 15.0f)]
	public float rotationSpeed = 10f;

	[Range (0.0f, 10.0f)]
	public float strafeSpeed = 1.0f;

	[Range (10.0f, 30.0f)]
	public float strafeReduction = 20f;

	private float rotationAngle = 0f;

	[Range (0.0f, 1f)]
	public float forwardSpeed = 0.5f;

	[Range (0.01f, 0.3f)]
	public float deadZone = 0.1f;

	[Range (0f, 100f)]
	public float maxSpeed = 10f;

	[Range (0.0f, 1.0f)]
	public float yThreshold = 0.4f;

	[Range (0.0f, 10.0f)]
	public float boostAmount = 8f;

	[Range (1.0f, 10.0f)]
	public float brakeAmount = 5f;

	[Range (0.1f, 1000)]
	public float breakForce = 200;

	[Range (0.0f, 1f)]
	public float breakMultiplier1 = 0.3f;

	[Range (0.0f, 1f)]
	public float breakMultiplier2 = 0.2f;

	[Range (0.0f, 1f)]
	public float breakMultiplier3 = 0.1f;

	[Range (0.0f, 100.0f)]
	public float brakeForce = 10f;

	[Range (1, 10)]
	public int brakeCooldown = 5;

	public GameObject[] carriable;

	public Rigidbody body;
	private float oldY;
	private bool boost = false;
	private bool brake = false;

	private bool jumping = false;

	void Start ()
	{

		carriable [0].GetComponent<FixedJoint> ().breakForce = breakForce;
		carriable [0].GetComponent<FixedJoint> ().breakTorque = breakForce;

		carriable [1].GetComponent<CarriableCollider> ().nextBreakForce = breakMultiplier1;
		carriable [1].GetComponent<CarriableCollider> ().nextBreakTorque = breakMultiplier1;

		carriable [2].GetComponent<CarriableCollider> ().nextBreakForce = breakMultiplier2;
		carriable [2].GetComponent<CarriableCollider> ().nextBreakTorque = breakMultiplier2;

		carriable [3].GetComponent<CarriableCollider> ().nextBreakForce = breakMultiplier3;
		carriable [3].GetComponent<CarriableCollider> ().nextBreakTorque = breakMultiplier3;
	}

	void OnEnable ()
	{
		EventManager.StartListening (GameManager.Instance._eventsContainer.obstacleHit, Jump);
		EventManager.StartListening (GameManager.Instance._eventsContainer.brakeEvent, Brake);
	}

	void OnDisable ()
	{
		EventManager.StopListening (GameManager.Instance._eventsContainer.obstacleHit, Jump);
		EventManager.StopListening (GameManager.Instance._eventsContainer.brakeEvent, Brake);
	}

	void FixedUpdate ()
	{
		Move ();
	}

	void Brake(){
		if (!brake) {
			brake = true;
			StartCoroutine (Flip (brakeCooldown));
			body.AddForce (new Vector3 (0f, 0f, -brakeForce), ForceMode.VelocityChange);
		}
	}

	void Move ()
	{

		/* SIDE MOVEMENT */

		float sideSpeed = Mathf.Abs (Input.acceleration.x) > deadZone ? Input.acceleration.x : 0;
		var target = Quaternion.Euler (0, 0, -sideSpeed * rotationMultiplier);

		body.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * rotationSpeed);
	
		if (transform.rotation.eulerAngles.z > 180) {
			rotationAngle = 360 - transform.rotation.eulerAngles.z;
		} else {
			rotationAngle = -transform.rotation.eulerAngles.z;
		}

		rotationAngle /= 500;
		rotationAngle *= strafeSpeed;
//		print (rotationAngle);
		var x = Mathf.Abs (rotationAngle) > deadZone / 500 && Mathf.Abs (body.velocity.x) > 0.00025 ? body.velocity.x + rotationAngle : 0;

		body.velocity = new Vector3 (x, body.velocity.y, body.velocity.z);
		//transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + rotationAngle, transform.position.y, transform.position.z), 1/strafeReduction);

		/* SPEED-UP / BRAKE MOVEMENT */

		oldY = oldY == 0 ? Input.acceleration.y : oldY;

		if (body.velocity.z < (boost ? maxSpeed + boostAmount : maxSpeed)) {
			body.AddForce (new Vector3 (0f, 0f, (boost ? forwardSpeed + 2 : forwardSpeed)), ForceMode.VelocityChange);	
		} else if (body.velocity.z < (brake ? maxSpeed - brakeAmount : maxSpeed)) {
			body.AddForce (new Vector3 (0f, 0f, forwardSpeed), ForceMode.VelocityChange);
		}

		oldY = Input.acceleration.y;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Ground") {
			jumping = false;
		}
	}

	void Jump ()
	{
		if(!jumping){
			switch (GameManager.Instance.nodgeDirection) {
				case NodgeDirection.Left:
					body.AddForce (new Vector3 (-GameManager.Instance.nodgeForce, GameManager.Instance.obstacleForceAddUp, 0), ForceMode.VelocityChange);
					break;
				case NodgeDirection.Right:
					body.AddForce (new Vector3 (GameManager.Instance.nodgeForce, GameManager.Instance.obstacleForceAddUp, 0), ForceMode.VelocityChange);
					break;
				default:
					body.AddForce (new Vector3 (0, GameManager.Instance.obstacleForceAddUp, 0), ForceMode.VelocityChange);
					break;
			}
			jumping = true;
			body.AddForce (new Vector3 (0, 0, -GameManager.Instance.obstacleBrakeForce), ForceMode.VelocityChange);
		}
	}

	IEnumerator Flip (int waitSec)
	{
		yield return new WaitForSeconds (waitSec);
		boost = false;
		brake = false;
	}

	IEnumerator ApplyBreakForce(int waitSec){
		yield return new WaitForSeconds (waitSec);

		carriable [0].GetComponent<CarriableCollider> ().ChangeBreakForce (breakForce, breakForce);

		carriable [1].GetComponent<CarriableCollider> ().nextBreakForce = breakMultiplier1;
		carriable [1].GetComponent<CarriableCollider> ().nextBreakTorque = breakMultiplier1;

		carriable [2].GetComponent<CarriableCollider> ().nextBreakForce = breakMultiplier2;
		carriable [2].GetComponent<CarriableCollider> ().nextBreakTorque = breakMultiplier2;

		carriable [3].GetComponent<CarriableCollider> ().nextBreakForce = breakMultiplier3;
		carriable [3].GetComponent<CarriableCollider> ().nextBreakTorque = breakMultiplier3;
	}
}
