using UnityEngine;
using System.Collections;

public class RaycastTracing : MonoBehaviour {

	public float maxRayDistance = 10;
	ArrayList GOList = new ArrayList();

	void FixedUpdate(){
		Ray ray = new Ray (transform.position, Vector3.forward);
		RaycastHit hit;
		Debug.DrawLine (transform.position, transform.position + Vector3.forward * maxRayDistance, Color.red);

		if (Physics.Raycast (ray, out hit, maxRayDistance)) {
			Debug.DrawLine (hit.point, hit.point + Vector3.up * 5, Color.green);
			if (hit.collider.gameObject.tag == "Box collider") {
				Debug.Log ("hit");
			}
		}


	}
		

}
