using UnityEngine;
using System.Collections;

public class CarriableCollider : MonoBehaviour {

	public float secondsToDestroy = 2;

	/// <summary>
	/// Raises the collision enter event for the Carriable prefab. 
	/// Ignores all collisions except for collisions with the ground, and triggers the LoseCarriableEvent.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag ("Ground")) {
			HandleCollision();
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
}
	