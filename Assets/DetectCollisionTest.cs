using UnityEngine;
using System.Collections;

public class DetectCollisionTest : MonoBehaviour {



	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.CompareTag("Obstacles"))
		{
			EventManager.TriggerEvent ("HitObstacle");
		}
	}
}
