using UnityEngine;
using System.Collections;

public class GravelCollider : MonoBehaviour {

	public float force = 0;

	void OnCollisionEnter(Collision collision) {
		GameManager.Instance.obstacleForceAddUp = force;
//		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.obstacleHit);
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.shakeCamera);
	}
}
