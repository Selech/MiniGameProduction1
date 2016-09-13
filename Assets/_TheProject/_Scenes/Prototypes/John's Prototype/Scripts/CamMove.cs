using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	public Transform target;
	[Range(1,20)] public float height = 5.0f;
	public bool isCameraSet = true;
	private Vector3 offset;
	private Vector3 prevPosition;


	[Range(0.05f,0.5f)] public float shakeForce = 0.1f;

	void Start()
	{
		EventManager.StartListening (GameManager.Instance._eventsContainer.shakeCamera, ShakeCamera);
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			EventManager.TriggerEvent (GameManager.Instance._eventsContainer.shakeCamera);
		}

	}

	void Update () 
	{
		if (target != null) {
//			Follow ();
//			if (!isCameraSet) {
			if (prevPosition != transform.position) {
				SetUpCamera ();
				//Follow ();
			}
//				else
//					isCameraSet = true;
//			}
//			else
//				Follow ();


		} else {
			print ("no target assigned for camera to follow");
		}
		EventManager.StopListening (GameManager.Instance._eventsContainer.shakeCamera, ShakeCamera);
	}

	void SetUpCamera()
	{
		Vector3 wantedPosition = target.position;
		wantedPosition.y = height;
		wantedPosition.x = wantedPosition.x - 10;
		wantedPosition.z = 5.0f;
		prevPosition = transform.position;
		transform.position = Vector3.MoveTowards(transform.position, wantedPosition, 0.5f* Time.fixedDeltaTime);
		transform.LookAt (target, target.up);
		Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
		wantedRotation.z = 0;
		wantedRotation.y = 30;

		transform.rotation = Quaternion.Slerp(transform.rotation,  wantedRotation, 0.4f* Time.fixedDeltaTime);
		offset = transform.position - target.transform.position; 
	}

	void Follow()
	{
		transform.position = target.transform.position + offset;
	}

	private void ShakeCamera(){
		var random = Random.Range (0,1);
		var shakeVector = random == 0 ? new Vector3 (shakeForce,shakeForce,shakeForce) : new Vector3 (-shakeForce,-shakeForce,-shakeForce);
//		transform.Translate (this.transform.position + shakeVector);
	}
}

public class CamMove : MonoBehaviour {

	public Transform target;
	[Range(1,20)] public float height = 4.0f;
	public bool isCameraSet = false;
	private Vector3 offset;
	private Vector3 prevPosition;


	[Range(0.05f,0.5f)] public float shakeForce = 0.1f;

	void Start()
	{
		EventManager.StartListening (GameManager.Instance._eventsContainer.shakeCamera, ShakeCamera);
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag ("Basket").transform;
			EventManager.TriggerEvent (GameManager.Instance._eventsContainer.shakeCamera);
		}

	}

	void FixedUpdate () 
	{
		if (target != null) {
			if (!isCameraSet) {
				if (prevPosition != transform.position)
					SetUpCamera ();
				else
					isCameraSet = true;
			}
			else
				Follow ();

		} else {
			print ("no target assigned for camera to follow");
		}
		EventManager.StopListening (GameManager.Instance._eventsContainer.shakeCamera, ShakeCamera);
	}

	void SetUpCamera()
	{
		Vector3 wantedPosition = target.position;
		wantedPosition.y = height;
		wantedPosition.x = wantedPosition.x - 10;
		prevPosition = transform.position;
		transform.position = Vector3.MoveTowards(transform.position, wantedPosition, 5.0f* Time.deltaTime);
		transform.LookAt (target, target.up);
		offset = transform.position - target.transform.position; 
	}

	void Follow()
	{
		transform.position = target.transform.position + offset;
	}

	private void ShakeCamera(){
		print ("Stuff");
		var random = Random.Range (0,1);
		var shakeVector = random == 0 ? new Vector3 (shakeForce,shakeForce,shakeForce) : new Vector3 (-shakeForce,-shakeForce,-shakeForce);
		transform.Translate (this.transform.position + shakeVector);
	}
}
