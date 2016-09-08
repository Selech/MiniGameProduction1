using UnityEngine;
using System.Collections;

public class CarriableCollider : MonoBehaviour {


	/// <summary>
	/// Raises the collision enter event for the Carriable prefab. 
	/// Ignores all collisions except for collisions with the ground, and triggers the LoseCarriableEvent.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Ground")) {
			EventManager.TriggerEvent ("LoseCarriableEvent");
		}
	}
}
	