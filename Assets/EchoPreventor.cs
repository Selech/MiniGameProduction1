using UnityEngine;
using System.Collections;

public class EchoPreventor : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		var col = this.GetComponent<BoxCollider> ();
		Destroy (col);
	}

}
