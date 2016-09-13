using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarriableManager : MonoBehaviour {
	
	public string[] carriablefromscene1; 

	private static CarriableManager _instance;
	public static CarriableManager Instance
	{
		get
		{ 
			if(_instance == null)
			{
				GameObject go = new GameObject ("CarriableManager");
				go.AddComponent<CarriableManager> ();
			}
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad (this);
	}

	public void convertToStringArray(List<GameObject> carriables) {

		carriablefromscene1 = new string[carriables.Count];
		var index = 0;

		foreach (var obj in carriables) {
			carriablefromscene1[index] = obj.name;
			index++;
		}

		foreach (var str in carriablefromscene1) {
			Debug.Log (str);
		}
	}

}
