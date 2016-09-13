using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CarrierManager : MonoBehaviour {

	GameObject[] objects;

	void OnEnable () {
		EventManager.StartListening(GameManager.Instance._eventsContainer.beginGame, AddJoints);
	}

	void OnDisable () {
		EventManager.StopListening(GameManager.Instance._eventsContainer.beginGame, AddJoints);
	}


	private void AddJoints(){
		objects = GameObject.FindGameObjectsWithTag ("Carriable");
		int size = objects.Length;
		for (int i = 0; i < size - 1; i++) {
			HingeJoint joint = objects [i].AddComponent<HingeJoint> ();
			joint.connectedBody = objects [i + 1].GetComponent<Rigidbody>();
			JointLimits limits = joint.limits;
			limits.max = 3;
			joint.limits = limits;
			joint.useLimits = true;
		}
	}
}
