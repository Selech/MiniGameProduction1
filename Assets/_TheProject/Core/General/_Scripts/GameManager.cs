using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState
{
	Playing,
	Paused
}

[System.Serializable]
public class EventsContainer
{
	public string beginGame = "BeginGame";	
	public string obstacleHit = "ObstacleHitEvent";
	public string resetGame = "ResetGame";
	public string loseCarriable = "LoseCarriableEvent";
	public string pauseGame = "PauseGame";
	public string resumeGame = "ResumeGame";
}

public class GameManager : MonoBehaviour {

	#region Variables

	public GameState _GameState;
	[SerializeField]
	public EventsContainer _eventsContainer = new EventsContainer();

	[HideInInspector] public float currentTime;
	public float maxTimeCompletion = 10f;

	[Range(0,10)] public int startCarriablesAmount=3;
	public int currentCarriablesAmount;
	public Transform startPositionSpawn;

	[HideInInspector] public bool isPaused = false;
	[HideInInspector] public bool hasGameStarted = false;
	public Text HUD_TimeText;

	//used for testing
	public string pauseGameButton = "Cancel";

	#region data refs

	public float obstacleForceAddUp;

	#endregion

	#endregion

	#region Sinleton Setup

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
		EventManager.StartListening (_eventsContainer.loseCarriable,LoseCarriable);
		EventManager.StartListening (_eventsContainer.obstacleHit,RestartGame);
	}

	void OnDisable()
	{
		EventManager.StopListening (_eventsContainer.loseCarriable,LoseCarriable);
		EventManager.StopListening (_eventsContainer.obstacleHit,RestartGame);
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
		EventManager.TriggerEvent (_eventsContainer.beginGame);
		hasGameStarted = true;
		InitGamePlayResume ();
		SpawnPlayer ();
	}

	public void RestartGame()
	{
		ResetSettings ();
		EventManager.TriggerEvent (_eventsContainer.resetGame);
	}

	//toggle paused game on input
	public void TogglePause()
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
		EventManager.TriggerEvent (_eventsContainer.pauseGame);
		InitGamePlayPause ();
	}

	void ResumeGame()
	{
		EventManager.TriggerEvent (_eventsContainer.resumeGame);
		InitGamePlayResume ();
	}

	void SpawnPlayer()
	{
		//reset carriables and time
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = startPositionSpawn.position;
		player.transform.rotation = startPositionSpawn.rotation;
		currentCarriablesAmount = startCarriablesAmount;
	}

	//reset settings on player spawn
	void ResetSettings()
	{
		isPaused = false;
		hasGameStarted = false;

		currentTime = 0;
		Time.timeScale = 1;
		SpawnPlayer ();
	}

	//change game state
	void InitGamePlayPause(){
		_GameState = GameState.Paused;
		Time.timeScale = 0;
	}

	//change game state
	void InitGamePlayResume(){
		_GameState = GameState.Playing;
		Time.timeScale = 1;
	}
	#endregion

	#region GamePlay Methods
	//once game has began, start calculating time
	void UpdateTime()
	{
		currentTime += Time.time;
		HUD_TimeText.text = "Elapsed Time "+(currentTime/60).ToString ("F1");


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
			
			currentCarriablesAmount = 0;
			RestartGame ();
		}
	}
	#endregion


	#endregion
}
