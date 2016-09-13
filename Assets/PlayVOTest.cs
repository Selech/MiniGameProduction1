using UnityEngine;
using System.Collections;

public class PlayVOTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			GameManager.Instance.PlayVO (1);
		} else if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			GameManager.Instance.PlayVO (2);
		}
		else if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			GameManager.Instance.PlayVO (3);
		}
		else if(Input.GetKeyDown(KeyCode.Keypad4))
		{
			GameManager.Instance.PlayVO (4);
		}
	}
}
