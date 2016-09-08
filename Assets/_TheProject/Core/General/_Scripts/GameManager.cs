using UnityEngine;
using System.Collections;

public enum GameState
{
	Playing,
	Paused
}

public class GameManager : MonoBehaviour {

	#region Variables

	public GameState _GameState;

	public float currentTime;
	public int currentCarriablesAmount=3;
	public Transform startPos;

	public bool isPaused = false;
	public bool hasGameStarted = false;
	#endregion

	#region Sinleton Methods

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{ 
			if(_instance == null)
			{
				GameObject go = new GameObject ("GameManager");
				go.AddComponent<GameManager> ();
			}
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;

	}

	#endregion

	#region System Methods
	// Use this for initialization
	void Start () 
	{
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hasGameStarted)
		{
			UpdateTime ();
		}
	}
	#endregion

	#region Custom Methods

	// begin game once all references have been made
	void StartGame()
	{
		EventManager.TriggerEvent ("BeginGame");
		hasGameStarted = true;
	}


	void SpawnPlayer()
	{
		//reset carriables and time
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = startPos.position;
		player.transform.rotation = startPos.rotation;
	}

	//reset settings on player spawn
	void ResetSettings()
	{
		isPaused = false;
		hasGameStarted = false;
		currentTime = 0;
	}

	//once game has began, start calculating time
	void UpdateTime()
	{
		currentTime += Time.time;
	}

	//decrement carriable counter
	void LoseCarriable()
	{
		currentCarriablesAmount--;
		CheckForEndGame ();
	}

	//check if counter is 0 so that the game can be reset
	void CheckForEndGame()
	{
		if(currentCarriablesAmount<=0)
		{
			ResetSettings ();
			EventManager.TriggerEvent ("ResetGame");
		}
	}

	#endregion
}
