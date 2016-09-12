using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour {

	public bool destroyOnCollision = true;
	public float force = 0;

	void OnTriggerEnter(Collider other) {
		GameManager.Instance.obstacleForceAddUp = force;
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.obstacleHit);
		this.gameObject.SetActive (!destroyOnCollision);  
	}
}
