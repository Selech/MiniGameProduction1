using UnityEngine;
using System.Collections;

public class Test_PlayerMovement : MonoBehaviour {
	
	public float speed = 10;
	public float tilt = 3;

	Rigidbody rigid;
	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 1);
		rigid.velocity = movement * speed;

		rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.velocity.x * -tilt);

	}
}
