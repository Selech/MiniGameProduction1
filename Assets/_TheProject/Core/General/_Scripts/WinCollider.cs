using UnityEngine;
using System.Collections;

public class WinCollider : MonoBehaviour {

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.CompareTag ("Player")) {
			collision.gameObject.GetComponent<PlayerControllerv2> ().forwardSpeed = 0f;
			//collision.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 ();
			collision.gameObject.GetComponentInChildren<Animator> ().SetTrigger ("WinAnimation");

			EventManager.TriggerEvent (GameManager.Instance._eventsContainer.winGame);
		}
	}
}
