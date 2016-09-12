using UnityEngine;
using System.Collections;

public class TraySpawn : MonoBehaviour {

	public GameObject stack;
	public Transform position;

	void Start(){
		Instantiate (stack,position.position,Quaternion.identity);
	}

}
