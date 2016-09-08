using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	public GameObject player;
	public float cameraHeight = -3f;

	void FixedUpdate() {
		Vector3 pos = player.transform.position;
		pos.z += cameraHeight;
		pos.y = 2.5f;
		transform.position = pos;
	}
}
