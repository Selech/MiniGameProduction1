using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnBikeDetector : MonoBehaviour {

	public ArrayList GOList = new ArrayList();

	void OnTriggerEnter(Collider other){
		if (!GOList.Contains (other.gameObject.name)) {
			GOList.Add (other.gameObject.name);
		}
		Debug.Log (GOList.Count);
		foreach (var o in GOList) {
			Debug.Log (o);
		}	
	}

	void OnTriggerExit(Collider other){
		if (GOList.Contains (other.gameObject.name)) {
			GOList.Remove (other.gameObject.name);
		}
		Debug.Log (GOList.Count);
		foreach (var o in GOList) {
				Debug.Log (o);
		}	
	}

}
