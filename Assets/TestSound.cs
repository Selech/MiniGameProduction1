using UnityEngine;
using System.Collections;

public class TestSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			AkSoundEngine.PostEvent ("Play_Bell",this.gameObject);
		}
	}
}
