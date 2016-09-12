using UnityEngine;
using System.Collections;

public class StackScript : MonoBehaviour {

	private bool insideCollider = false;

	void OnTriggerEnter(Collider other){
		insideCollider = true;
	}

	void OnTriggerExit(Collider other){
		insideCollider = false;
	}


}
