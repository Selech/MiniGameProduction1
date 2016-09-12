using UnityEngine;
using System.Collections;

public class GameplayCanvasController : MonoBehaviour {

	public void Brake(){
		EventManager.TriggerEvent (GameManager.Instance._eventsContainer.brakeEvent);
	}
}
