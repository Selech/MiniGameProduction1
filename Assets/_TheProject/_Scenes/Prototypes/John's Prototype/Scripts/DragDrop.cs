using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	Vector3 dist;
	float posX;
	float posY;
	OnBikeDetector obd;
	public Transform MiddleofBike;
	public float LerpSpeed = 2;

	[Range (0.1f, 2.0f)]
	public float translateSpeed = 0.1f;

	protected Vector3 initPosition;
	private Vector3 initScreenPosition;

	public float heightOfObject;

	public bool inDragScene = false;

	void OnEnable(){
		if (inDragScene) {
			MiddleofBike = GameObject.FindGameObjectWithTag ("MiddleOfBike").transform;
			obd = GameObject.FindGameObjectWithTag ("CarriableDetector").GetComponent<OnBikeDetector> ();
		}
	}

	void Start() {
		initPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	void OnMouseDown() {
		dist = Camera.main.WorldToScreenPoint (transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
	}

	void OnMouseDrag(){
		Vector3 curPos= new Vector3 (Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
		Vector3 worldPos =  Camera.main.ScreenToWorldPoint (curPos);
		transform.position = new Vector3(worldPos.x, worldPos.y, initPosition.z);
	}

	public void Sort(){
		StopAllCoroutines();
		StartCoroutine (MoveToFinalPosition (this.transform));
		obd.currentHeight += heightOfObject;
	}

	void OnMouseUp() {
		GameManager.Instance.PlayUISnap();
		RaycastHit[] testhit;
		Vector3 fwd = transform.TransformDirection (Vector3.forward);

		testhit = Physics.RaycastAll (transform.position, Vector3.forward, 100f);

		if (testhit.Length > 0 && obd.CollectedCarriables.Count < 4) {
			bool hitted = false;

			foreach (var hit in testhit) {
				if (hit.collider.gameObject.tag == "CarriableDetector") {
					hitted = true;
					StartCoroutine (MoveToFinalPosition(this.transform));

					if (obd != null) {
						obd.addObject (this.gameObject, heightOfObject);
					}
					break;
				}	
			}

			if (!hitted) {
				MoveToInitialPosition ();
			}
		} else {
			MoveToInitialPosition ();
		}
	}

	void MoveToInitialPosition() {
		StartCoroutine (MoveToInitialPosition(this.transform));
		if (obd != null) {
			obd.removeObject (this.gameObject, heightOfObject);
		}
	}

	IEnumerator MoveToFinalPosition(Transform transThis) {
		Debug.Log (obd.currentHeight);

		if (transThis != null) {
			this.GetComponent<BoxCollider> ().enabled = false;

			Vector3 newPosition = MiddleofBike.position + new Vector3 (0, obd.currentHeight, 0);
			float distance = Vector3.Distance (transThis.position, newPosition);

			while (distance > 0.1f) {
				transThis.position = Vector3.MoveTowards (transThis.position, newPosition, Time.deltaTime * 10);
				transThis.rotation = Quaternion.RotateTowards (transThis.rotation, MiddleofBike.rotation, Time.deltaTime * 1000);
				distance = Vector3.Distance (transThis.position, newPosition);
				yield return null;
			}

			this.GetComponent<BoxCollider> ().enabled = true;
			transThis.position = newPosition;
		}
	}

	IEnumerator MoveToInitialPosition(Transform transThis) {
		if (transThis != null) {
			this.GetComponent<BoxCollider> ().enabled = false;
			float distance = Vector3.Distance (transThis.position, initPosition);
			while (distance > 0.1f) {
				transThis.position = Vector3.MoveTowards (transThis.position, initPosition, Time.deltaTime * 10);
				transThis.rotation = Quaternion.RotateTowards (transThis.rotation, Quaternion.identity, Time.deltaTime * 500);
				distance = Vector3.Distance (transThis.position, initPosition);
				yield return null;
			}
			this.GetComponent<BoxCollider> ().enabled = true;
			transThis.position = initPosition;
		}
	}
}
