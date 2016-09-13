using UnityEngine;
using System.Collections;

public class CarriableCollider : MonoBehaviour {

	public float nextBreakForce = 1;
	public float nextBreakTorque = 1;
	public int secondsToEnableNext = 1;
	public float secondsToDestroy = 2;

	private bool lostCarriable = false;

	private float breakForce;
	private float breakTorque;

	void Start(){
		breakForce = GetComponent<FixedJoint>().breakForce;
		breakTorque = GetComponent<FixedJoint>().breakTorque;
	}

	public void ChangeBreakForce(float breakFor, float breakTor){
		breakForce = breakFor;
		breakTorque = breakTor;

		this.GetComponent<FixedJoint>().breakForce = breakFor;
		this.GetComponent<FixedJoint>().breakTorque = breakTor;
	}

	/// <summary>
	/// Raises the collision enter event for the Carriable prefab. 
	/// Ignores all collisions except for collisions with the ground, and triggers the LoseCarriableEvent.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter(Collision collision) {
		if ((collision.gameObject.CompareTag ("Ground") || collision.gameObject.CompareTag ("Obstacles")) && !lostCarriable) {
			HandleCollision();
			lostCarriable = true;
		}
	}

	/// <summary>
	/// Handles the collision.
	/// </summary>
	void HandleCollision()
	{
		StopCoroutine ("HandleCollisionCo");
		StartCoroutine ("HandleCollisionCo");
	}


	/// <summary>
	/// Coroutine that triggers the LoseCarriableEvent, waits 2 seconds, then destroys the game object.
	/// </summary>
	IEnumerator HandleCollisionCo()
	{
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.loseCarriable);
		yield return new WaitForSeconds(secondsToDestroy);
		Destroy(this.gameObject);
	}

	/// <summary>
	/// Raised by the system when the box falls by forces.
	/// </summary>
	void OnJointBreak(float breakForce)
	{
		GameObject attachedObject = GetComponent<FixedJoint>().connectedBody.gameObject;
		if(attachedObject.tag == "Carriable")
		{
			StartCoroutine(JointBreakCo(attachedObject));

		}
		this.gameObject.transform.SetParent (null);
		//Debug.Log("A joint has just been broken!, force: " + breakForce);
	}

	IEnumerator JointBreakCo(GameObject attachedObject)
	{
		//print(Time.time);
		print (secondsToEnableNext);
		yield return new WaitForSeconds(secondsToEnableNext);
		print ("after: " + breakForce);
		//after seconds are passed
		var joint = GetComponent<FixedJoint>();

		var newForce = breakForce * nextBreakForce;
		attachedObject.GetComponent<CarriableCollider> ().ChangeBreakForce (newForce, newForce);

		//print(Time.time);
	}

}




