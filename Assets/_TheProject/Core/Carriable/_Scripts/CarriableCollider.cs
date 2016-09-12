using UnityEngine;
using System.Collections;

public class CarriableCollider : MonoBehaviour {

	public int nextBreakForce = 1;
	public int nextBreakTorque = 1;
	public int secondsToEnableNext = 2;
	public float secondsToDestroy = 2;

	/// <summary>
	/// Raises the collision enter event for the Carriable prefab. 
	/// Ignores all collisions except for collisions with the ground, and triggers the LoseCarriableEvent.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag ("Ground")) {
			//TO REENABLE LATER	HandleCollision();
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
		EventManager.TriggerEvent ("LoseCarriableEvent");
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
		//Debug.Log("A joint has just been broken!, force: " + breakForce);
	}

	IEnumerator JointBreakCo(GameObject attachedObject)
	{
		print(Time.time);
		yield return new WaitForSeconds(secondsToEnableNext);
		//after seconds are passed
		attachedObject.GetComponent<FixedJoint>().breakForce = nextBreakForce;
		attachedObject.GetComponent<FixedJoint>().breakTorque = nextBreakTorque;
		print(Time.time);
	}

}




