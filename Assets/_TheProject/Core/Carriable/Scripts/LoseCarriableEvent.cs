using UnityEngine;
using System.Collections;

public class LoseCarriableEvent : MonoBehaviour {

	void OnEnable()
	{
		EventManager.StartListening ("LoseCarriableEvent", LoseCarriable);
	}

	void OnDisable()
	{
		EventManager.StopListening ("LoseCarriableEvent", LoseCarriable);
	}

	void LoseCarriable() {

	}
}
