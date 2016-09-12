using UnityEngine;
using System.Collections;

public class StartBike : MonoBehaviour {

	public GameObject bike;
	bool start = false;
	public float speed = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.E)) 
		{

			start = true;

		}
		if (start) 
		{
			bike.transform.position = Vector3.Lerp (bike.transform.position, new Vector3 (14f, 1.69f, 0),speed * Time.deltaTime);
		}

	}
}
