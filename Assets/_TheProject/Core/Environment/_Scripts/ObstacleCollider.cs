using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour {

	public bool destroyOnCollision = true; 

	[Range(0.0f, 100.0f)]
	public float jumpForce = 0;

	[Range(0.0f, 100.0f)]
	public float brakeForce = 0;

	public NodgeDirection nodgeDirection = NodgeDirection.Disabled;

	[Range(0.0f, 100.0f)]
	public float nodgeForce = 0;

	[Range(0.0f, 5f)]
	public float nodgeCooldown = 0;

	void OnTriggerEnter(Collider other) {
		GameManager.Instance.obstacleForceAddUp = jumpForce;
		GameManager.Instance.obstacleBrakeForce = brakeForce;
		GameManager.Instance.nodgeDirection = nodgeDirection;
		GameManager.Instance.nodgeForce = nodgeForce;
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.obstacleHit);

		if (nodgeDirection != NodgeDirection.Disabled && GameManager.Instance.nodgeActive) {
			EventManager.TriggerEvent (GameManager.Instance._eventsContainer.curbHit);
			GameManager.Instance.nodgeActive = false;

			if (nodgeCooldown != 0f) {
				StartCoroutine (NodgeCooldown (nodgeCooldown));
			} else {
				GameManager.Instance.nodgeActive = true;
			}
		}

		this.gameObject.SetActive (!destroyOnCollision);  
	}

	IEnumerator NodgeCooldown (float waitSec)
	{
		yield return new WaitForSeconds (waitSec);
		GameManager.Instance.nodgeActive = true;
	}
}
