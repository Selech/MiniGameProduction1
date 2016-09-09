using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	[HideInInspector]public Transform target;
	[Range(1,20)] public float distance = 3.0f;
	[Range(1,20)] public float height = 3.0f;
	[Range(0.1f,30)] public float damping = 5.0f;
	[Range(0.1f,30)] public float rotationDamping = 10.0f;
	public bool smoothRotation = true;
	public bool followBehind = true;

	void OnEnable()
	{
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	void FixedUpdate () 
	{
		if (target != null) {
			Follow ();
			
		} else {
			print ("no target assigned for camera to follow");
		}
	}

	void Follow()
	{
		Vector3 wantedPosition;
		if(followBehind)
			wantedPosition = target.TransformPoint(0, height, -distance);
		else
			wantedPosition = target.TransformPoint(0, height, distance);

		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

		if (smoothRotation) {
			Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			wantedRotation.z = 0;

			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		}
		else transform.LookAt (target, target.up);
	}
}
