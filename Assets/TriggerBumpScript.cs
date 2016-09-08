using UnityEngine;
using System.Collections;

public class TriggerBump : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			Destroy(col.gameObject);

			//player.GetComponent<PlayerController> ().jump ();

		}
	}

}
