using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JointCreation : MonoBehaviour {

	public GameObject sofa;
	public GameObject speaker;
	public GameObject painting;
	public GameObject pillar;
	public GameObject bucket;
	public GameObject fridge;

	public GameObject parent;
	public PlayerControllerv2 player;

	public List<GameObject> gameObjectStack = new List<GameObject>();
	private float height = 0;

	// Use this for initialization
	void Start () {
		gameObjectStack.Add (this.gameObject);

		GameObject ob = null;

		int index = player.stack.Length-1;
		foreach (var car in player.stack) {
			switch (car) {
			case "sofa":
				ob = (GameObject) Instantiate (sofa, this.transform.position + new Vector3(0,height,0), Quaternion.identity);
				break;
			case "fridge":
				ob = (GameObject) Instantiate (fridge, this.transform.position + new Vector3(0,height,0), Quaternion.identity);
				break;
			case "bucket":
				ob = (GameObject) Instantiate (bucket, this.transform.position + new Vector3(0,height,0), Quaternion.identity);
				break;
			case "pillar":
				ob = (GameObject) Instantiate (pillar, this.transform.position + new Vector3(0,height,0), Quaternion.identity);
				break;
			case "speaker":
				ob = (GameObject) Instantiate (speaker, this.transform.position + new Vector3(0,height,0), Quaternion.identity);
				break;
			case "painting":
				ob = (GameObject) Instantiate (painting, this.transform.position + new Vector3(0,height,0), Quaternion.identity);
				break;
			default:
				break;
			}

			height += ob.GetComponent<DragDrop> ().heightOfObject;
			var joint = ob.AddComponent<FixedJoint> ();
			joint.connectedBody = gameObjectStack [gameObjectStack.Count - 1].gameObject.GetComponent<Rigidbody>();
			var col = ob.AddComponent<CarriableCollider> ();
			col.secondsToDestroy = 2;
			col.secondsToEnableNext = 1;

			ob.transform.SetParent (parent.transform);
			player.carriable [index] = ob;

			var script = ob.GetComponent<DragDrop> ();
			Destroy (script);
			ob.tag = "Carriable";

			gameObjectStack.Add (ob);
			index--;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
