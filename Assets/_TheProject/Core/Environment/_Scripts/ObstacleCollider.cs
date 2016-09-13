using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour {

	public bool destroyOnCollision = true; 

	[Range(0.0f, 100.0f)]
	public float jumpForce = 0;

	[Range(0.0f, 100.0f)]
	public float brakeForce = 0;

	public GameObject particles;

	void OnTriggerEnter(Collider other) {
		GameManager.Instance.obstacleForceAddUp = jumpForce;
		GameManager.Instance.obstacleBrakeForce = brakeForce;
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.obstacleHit);
		Instantiate (particles, this.transform.position, Quaternion.identity);
		this.gameObject.SetActive (!destroyOnCollision);  
	}
}
