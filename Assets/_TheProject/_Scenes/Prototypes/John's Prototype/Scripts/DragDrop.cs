using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	Vector3 dist;
	float posX;
	float posY;
	OnBikeDetector obd;
	public Transform MiddleofBike;
	public float LerpSpeed = 2;

	void OnEnable(){
		MiddleofBike = GameObject.FindGameObjectWithTag ("MiddleOfBike").transform;
		obd = GameObject.FindGameObjectWithTag("CarraibleCollector").GetComponent<OnBikeDetector> ();
	}


	void OnMouseDown(){
		dist = Camera.main.WorldToScreenPoint (transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
		GetComponent<Rigidbody>().detectCollisions = false;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	void OnMouseDrag(){
		Vector3 curPos= new Vector3 (Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
		Vector3 worldPos =  Camera.main.ScreenToWorldPoint (curPos);
		transform.position = worldPos;
	}

	void OnMouseUp(){
		
		GetComponent<Rigidbody>().detectCollisions = true;
		GetComponent<Rigidbody>().isKinematic = false;
//		if(obd.CurrentCarriable != null){
//			StartCoroutine ("EndMoveObject", obd.CurrentCarriable.transform);
//		}
		//CheckIfInside ();
		CheckForBasket();
	}

	void CheckForBasket(){
		RaycastHit hit;

		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 100)) {
			if (hit.collider.tag == "Basket") {
				StartCoroutine ("EndMoveObject", this.transform);
			} else {
				print ("something " + hit.collider.tag);
			}
		}
	}

	IEnumerator EndMoveObject(Transform t){
		if (t != null) {
			Vector3 newPosition = new Vector3 (MiddleofBike.position.x, MiddleofBike.position.y + 0.5f * t.localScale.y, MiddleofBike.position.z);
			float distance = Vector3.Distance (t.position, newPosition);
			while (distance > 0.1f) {
				t.position = Vector3.MoveTowards (t.position, newPosition, Time.deltaTime * LerpSpeed);
				distance = Vector3.Distance (t.position, newPosition);
				yield return null;
			}
			t.position = newPosition;
		}
	}

//	void CheckIfInside(){
//		for(int i = 0; i<obd.CollectedCarriables.Count; i++){
//			Debug.Log ("blah");
//		}
//	
////		foreach (var v in obd.CollectedCarriables) {
////			Debug.Log ("inside foreach");
////			if (v == this.gameObject) {
////				Debug.Log ("inside if");
////
////			}
////		}
//
//
//
//	}

	void OnTriggerEnter(Collider other){
		print ("Collided");
		if (other.CompareTag ("CarraibleCollector") && GetComponent<Collider>().isTrigger) {
			//obd = other.GetComponent<OnBikeDetector> ();
			if (obd != null) {
				obd.CurrentCarriable = this.gameObject;
				obd.CollectedCarriables.Add (this.gameObject);
				print ("count: "+ obd.CollectedCarriables.Count);
			}

		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("CarraibleCollector") && GetComponent<Collider>().isTrigger) {
			//	insideCollider = false;

			if (obd != null) {
				foreach (var v in obd.CollectedCarriables) {
					if (this.gameObject == v) {
						obd.CollectedCarriables.Remove (this.gameObject);
						break;
					}
				}
				obd.CurrentCarriable = null;
			}
		}
	}
		
}
