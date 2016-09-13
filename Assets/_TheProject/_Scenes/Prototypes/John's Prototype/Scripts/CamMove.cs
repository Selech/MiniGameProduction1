using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	public Transform target;

	[Range(1,20)] public float height = 5.0f;
	public bool isCameraSet = true;

	private Vector3 offset;
	private Vector3 finalDestination = new Vector3(13f, 196f, 555f);
	public Transform destination;

	void Start() {
		if(target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			destination = GameObject.Find ("FinalDestination").transform;
		}
	}

	void Update () {
		if (target != null) {
			if (destination.position != target.position) {
				SetUpCamera ();
			}
		} else {
			print ("no target assigned for camera to follow");
		}
	}

	void SetUpCamera() {

		transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f* Time.fixedDeltaTime);
		transform.LookAt (target, target.up);

		Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position);
		wantedRotation.y += 0f;

		transform.rotation = Quaternion.Slerp(transform.rotation,  wantedRotation, 0.4f* Time.fixedDeltaTime);
		offset = transform.position - target.transform.position; 
	}
}
