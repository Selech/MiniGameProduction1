﻿using UnityEngine;
using System.Collections;

public class WinCollider : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag ("Player")) {
			GameManager.Instance.startPositionSpawn ();
		}
	}
}
