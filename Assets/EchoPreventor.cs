using UnityEngine;
using System.Collections;

public class EchoPreventor : MonoBehaviour {

	public string index;
	public string lang;

	void OnTriggerEnter(Collider other){
		GameManager.Instance.PlaySound ("Play_VO_" + index + lang );

		var col = this.GetComponent<BoxCollider> ();
		Destroy (col);
	}

}
