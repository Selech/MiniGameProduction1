using UnityEngine;
using System.Collections;

public class Test_PlayerMovement : MonoBehaviour {
	
	public float speed = 10;
	public float tilt = 3;
	float moveHorizontal;
	Rigidbody rigid;
	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

	}

	void Move()
	{
		if (Application.isEditor) {
			moveHorizontal = Input.GetAxis ("Horizontal");

		} else {
			moveHorizontal = Input.acceleration.x;
		}

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 1);
		rigid.velocity = movement * speed;

		rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.velocity.x * -tilt);
	}
}
