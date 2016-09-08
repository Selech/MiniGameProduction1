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

	[HideInInspector] public float currentTime;
	public float maxTimeCompletion = 1000f;

	[Range(1,10)] public int currentCarriablesAmount=3;
	public Transform startPos;

	[HideInInspector] public bool isPaused = false;
	[HideInInspector] public bool hasGameStarted = false;

	public string pauseGameButton = "Cancel";
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

	void OnEnable()
	{
		EventManager.StartListening ("LoseCarriableEvent",LoseCarriable);
	}

	void OnDisable()
	{
		EventManager.StopListening ("LoseCarriableEvent",LoseCarriable);
	}

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
			if(Input.GetButtonDown(pauseGameButton))
			{
				TogglePause ();
			}
		
			if(!isPaused)
			{
				UpdateTime ();
			}
		}
	}
	#endregion

	#region Custom Methods

	#region General Methods
	// begin game once all references have been made
	void StartGame()
	{
		EventManager.TriggerEvent ("BeginGame");
		hasGameStarted = true;
		InitGamePlayResume ();
	}

	void RestartGame()
	{
		ResetSettings ();
		EventManager.TriggerEvent ("ResetGame");
	}

	//toggle paused game on input
	void TogglePause()
	{
		
		isPaused = !isPaused;

		if(isPaused)
		{
			
			PauseGame ();
		}
		else 
		{
			ResumeGame();
		}

	}

	void PauseGame()
	{
		EventManager.TriggerEvent ("PauseGame");
		InitGamePlayPause ();
	}

	void ResumeGame()
	{
		EventManager.TriggerEvent ("ResumeGame");
		InitGamePlayResume ();
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

	//change game state
	void InitGamePlayPause(){
		_GameState = GameState.Paused;
	}

	//change game state
	void InitGamePlayResume(){
		_GameState = GameState.Playing;
	}
	#endregion

	#region GamePlay Methods
	//once game has began, start calculating time
	void UpdateTime()
	{
		currentTime += Time.time;

		if(currentTime>maxTimeCompletion)
		{
			RestartGame ();
		}
	}

	//decrement carriable counter
	public void LoseCarriable()
	{
		currentCarriablesAmount--;
		CheckForEndGame ();
	}

	//check if counter is 0 so that the game can be reset
	void CheckForEndGame()
	{
		if(currentCarriablesAmount<=0)
		{
			RestartGame ();
		}
	}
	#endregion


	#endregion
}
