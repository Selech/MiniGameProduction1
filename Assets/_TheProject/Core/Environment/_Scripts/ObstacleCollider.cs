using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour {

	public bool destroyOnCollision = true;
	public float force = 0;

	void OnCollisionEnter(Collision collision) {
		GameManager.Instance.obstacleForceAddUp = force;
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.obstacleHit);
		this.gameObject.SetActive (!destroyOnCollision);  
	}
}
