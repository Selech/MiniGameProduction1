using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public Transform destination;
	public int distance = 10;
	public static GameObject levels;
	private bool isCameraSet = false;

	// Use this for initialization
	void Start () {
		destination = GameObject.Find ("FinalDestination").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(isCameraSet){
			if (Vector3.Distance (transform.position, destination.position) > 0.5) {
				Vector3 wantedPosition = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z);
				transform.position = Vector3.MoveTowards (transform.position, wantedPosition, Time.deltaTime * 0.5f);
			}
			else {
				levels = GameObject.Find("Canvas").transform.FindChild("Levels").gameObject;
				if (levels != null)
					levels.SetActive (true);
				else
					GameObject.Find ("DebugText").GetComponent<Text> ().text = "null";
			}
		}
		else isCameraSet = GameObject.Find ("Main Camera").GetComponent<CamMove>().isCameraSet;
	}
}
