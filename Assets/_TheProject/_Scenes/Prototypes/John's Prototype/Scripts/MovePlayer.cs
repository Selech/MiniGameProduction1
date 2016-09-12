using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	private bool isCameraSet = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(isCameraSet){
			Vector3 wantedPosition = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * 2.5f);
		}
		else isCameraSet = GameObject.Find ("Main Camera").GetComponent<CamMove>().isCameraSet;
	}
}
