using UnityEngine;
using System.Collections;

public class Test_PlayerMovement : MonoBehaviour 
{

	[Range(0.01f,0.3f)]
	public float deadZone = 0.1f;
	[Range(1f,15f)]
	public float rotSpeed;
	public float speed = 10;
	public float forceUp = 5;
	public float tilt = 3;
	float moveHorizontal;
	Rigidbody rigid;
	public bool hasRemote = true;
	[HideInInspector] public float curIn = 0;
	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody> ();
	}

	void OnEnable()
	{
		//EventManager.StartListening ("LoseCarriableEvent",LoseCarriable);
		EventManager.StartListening (GameManager.Instance._eventsContainer.obstacleHit,Collide);
	}

	void OnDisable()
	{
		//EventManager.StopListening ("LoseCarriableEvent",LoseCarriable);
		EventManager.StopListening (GameManager.Instance._eventsContainer.obstacleHit,Collide);
	}

	// Update is called once per frame
	void Update () 
	{
		Move ();

	}

	void Move()
	{
		if (!hasRemote) {
			if (Application.isEditor) {
				moveHorizontal = Input.GetAxis ("Horizontal");
				
			} else {
				moveHorizontal = Input.acceleration.x;
			}
			
		} else {
			moveHorizontal = Input.acceleration.x;
		}

		float sideSpeed = Mathf.Abs (moveHorizontal) > deadZone ? moveHorizontal : 0;


		curIn = Mathf.MoveTowards (curIn,sideSpeed,rotSpeed*Time.deltaTime);

		Vector3 movement = new Vector3 (curIn, 0.0f, 1);
		rigid.velocity = movement * speed;

		rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.velocity.x * -tilt);
	}


	void Collide()
	{
		forceUp = GameManager.Instance.obstacleForceAddUp;
		rigid.AddForce (Vector3.up*forceUp,ForceMode.Impulse);
		//print ("hit obstaclez");
	}
}
