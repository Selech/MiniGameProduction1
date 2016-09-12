using UnityEngine;
using UnityEngine.SceneManagement;
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
	public string brakeEvent = "BrakeEvent";
	public string resetGame = "ResetGame";
	public string loseCarriable = "LoseCarriableEvent";
	public string pauseGame = "PauseGame";
	public string resumeGame = "ResumeGame";
	public string winGame = "WinGame";
	public string shakeCamera = "ShakeCamera";
}

[System.Serializable]
public class SoundEventsContainer
{
	public SoundItems_Collection _voiceOver_Collection;
	public SoundItems_Collection _soundItems_Collection;
	[Space(10)]
	public string beginGame = "BeginGame";	
	public string obstacleHit = "ObstacleHitEvent";
	public string resetGame = "ResetGame";
	public string loseCarriable = "LoseCarriableEvent";
	public string pauseGame = "PauseGame";
	public string resumeGame = "ResumeGame";
	public string winGame = "WinGame";
	public string shakeCamera = "ShakeCamera";
}


public class GameManager : MonoBehaviour {

	#region Variables

	public GameState _GameState;
	public GameObject playerPrefab;
	public GameObject playerCamera;
	public GameObject gameplayCanvas;
	public GameObject loseCanvas;
	public GameObject mainCanvas;

	private GameObject curPlayer;
	[Space(10)]
	[SerializeField]
	public EventsContainer _eventsContainer = new EventsContainer();

	[SerializeField]
	public SoundEventsContainer _soundEventsContainer  = new SoundEventsContainer();

	[HideInInspector] public float currentTime;
	[Space(10)]
	public float maxTimeCompletion = 10f;

	[Range(0,10)] public int startCarriablesAmount = 4;
	public int currentCarriablesAmount;
	public Transform startPositionSpawn;

	[HideInInspector] public bool isPaused = false;
	[HideInInspector] public bool hasGameStarted = false;
	public Text HUD_TimeText;

	//used for testing
	public string pauseGameButton = "Cancel";

	#region data refs

	public float obstacleForceAddUp;
	public float obstacleBrakeForce;

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
		EventManager.StartListening (_eventsContainer.loseCarriable, LoseCarriable);
		EventManager.StartListening (_eventsContainer.winGame, WinGame);
	}

	void OnDisable()
	{
		EventManager.StopListening (_eventsContainer.loseCarriable,LoseCarriable);
		EventManager.StopListening (_eventsContainer.winGame, WinGame);
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
		currentCarriablesAmount = startCarriablesAmount;
		gameplayCanvas.SetActive (true);
		EventManager.TriggerEvent (_eventsContainer.beginGame);
		hasGameStarted = true;
		InitGamePlayResume ();
		SpawnPlayer ();
	}

	public void RestartGame()
	{
		loseCanvas.SetActive (false);
		ResetSettings ();
		Time.timeScale = 1;
		hasGameStarted = true;
		EventManager.TriggerEvent (_eventsContainer.resetGame);
	}

	void WinGame() {
		RestartGame ();
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

	//reset settings on player spawn
	void ResetSettings()
	{
		isPaused = false;
		hasGameStarted = false;

		currentTime = 0.0f;
		HUD_TimeText.text = "Elapsed Time "+(currentTime).ToString ("F2");

		SpawnPlayer ();
		currentCarriablesAmount = startCarriablesAmount;
	}

	void SpawnPlayer()
	{

		if (curPlayer == null) {

			GameObject go = (GameObject)Instantiate (playerPrefab) as GameObject;
			GameObject cam = (GameObject)Instantiate (playerCamera) as GameObject;

			CamFollow cf = cam.GetComponent<CamFollow> ();
			cf.target = go.transform;

			//reset carriables and time
			//		GameObject player = GameObject.FindGameObjectWithTag("Player");
			curPlayer = go;
		} 
			
		curPlayer.transform.position = startPositionSpawn.position;
		curPlayer.transform.rotation = startPositionSpawn.rotation;
	}

	//change game state
	void InitGamePlayPause(){
		_GameState = GameState.Paused;
		Time.timeScale = Mathf.Epsilon;
	}

	//change game state
	void InitGamePlayResume(){
		_GameState = GameState.Playing;
		Time.timeScale = 1;
	}

	/// <summary>
	/// Loads the previous level.
	/// </summary>
	public void LoadPreviousLevel(){
		if (SceneManager.GetActiveScene().buildIndex != 0) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
		}
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel() {
		if (SceneManager.GetActiveScene ().buildIndex + 1 != SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}

	#endregion

	#region GamePlay Methods
	//once game has began, start calculating time
	void UpdateTime()
	{
		currentTime += Time.deltaTime;
		HUD_TimeText.text = "Elapsed Time "+(currentTime/60).ToString ("F2");


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
			PauseGame ();
			currentCarriablesAmount = 0;
			loseCanvas.SetActive (true);
		}
	}
	#endregion


	#endregion
}
