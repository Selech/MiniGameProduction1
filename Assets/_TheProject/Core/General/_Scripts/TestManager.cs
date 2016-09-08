using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

	public GameObject o;

	void OnEnable()
	{
		EventManager.StartListening ("TestEvent",SpawnShit);
	}

	void OnDisable()
	{
		EventManager.StopListening ("TestEvent",SpawnShit);
	}

	void SpawnShit()
	{
		if (o != null) {
			GameObject b = (GameObject)Instantiate (o, Vector3.zero, Quaternion.identity);
		} else {
			print ("no object to spawn");
		}
	}
}
