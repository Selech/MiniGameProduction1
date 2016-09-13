using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour
{

	public Transform target;

	[Range (1, 20)] public float height = 5.0f;
	public bool isCameraSet = true;

	private Vector3 offset;
	private Vector3 finalDestination = new Vector3 (13f, 196f, 555f);
	public Transform destination;

	private int countDown = 100;

	void Start ()
	{
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			destination = GameObject.Find ("FinalDestination").transform;
		}
	}

	void Update ()
	{
		var dist = Vector3.Distance (destination.position, target.position);
		if (target != null && dist > 0.1f) {
			SetUpCamera ();
		} 
	}

	void SetUpCamera ()
	{
		var positionTarget = (target.position - 6 * (destination.position - target.position).normalized) + new Vector3 (0, 1.5f, 0);
		transform.position = Vector3.MoveTowards (transform.position, positionTarget, 2f * Time.deltaTime);

		Quaternion wantedRotation = Quaternion.LookRotation (target.position - transform.position);
		wantedRotation.x += 10f;
		transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, 3f * Time.deltaTime);
	}
}
