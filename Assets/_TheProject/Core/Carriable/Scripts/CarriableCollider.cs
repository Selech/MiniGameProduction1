using UnityEngine;
using System.Collections;

public class CarriableCollider : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if (GameObject.FindWithTag ("Ground")) {
			Destroy (gameObject);
		}
	}
}
	