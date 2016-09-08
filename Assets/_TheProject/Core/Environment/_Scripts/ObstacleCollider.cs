using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		EventManager.TriggerEvent ("ObstacleHitEvent");
	}
}
