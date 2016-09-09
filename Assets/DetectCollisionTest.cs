using UnityEngine;
using System.Collections;

public class DetectCollisionTest : MonoBehaviour {

	public string obstacleTag = "Obstacles";
	public float bumpForceToAddUp = 10;

	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.CompareTag(obstacleTag))
		{
			//set up the jump force in the game manager which is then referenced from the player movement script when the hit obstacle event is executed
			GameManager.Instance.obstacleForceAddUp = bumpForceToAddUp;
			EventManager.TriggerEvent (GameManager.Instance._eventsContainer.loseCarriable);

		}
	}
}
