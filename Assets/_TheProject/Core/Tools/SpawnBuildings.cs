#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SpawnBuildings : EditorWindow {
	
	GameObject parent; 
	GameObject house0;
	GameObject house1;
	GameObject house2;
	GameObject house3;
	GameObject house4;
	GameObject house5;
	GameObject house6;
	GameObject house7;
	GameObject house8;
	GameObject house9;


	float positionJitter = 0f;
	float zPositionJitter = 0f;
	float rotateJitter = 0f;
	float houseWidth = 3f;
	int length = 20;
	float width = 3f;

	[MenuItem ("Edit/Generate buildings")]
	public static void MyWindow() {
		EditorWindow.GetWindow (typeof(SpawnBuildings));

	}

	public void spawnBuilding() 
	{ 
		if (parent != null) {
		for (int i = 0; i < length; i++) 
			{ 
				List<GameObject> houses = new List<GameObject> ();
				houses.AddRange (new GameObject[] {house0,house1,house2,house3,house4,house5,house6,house7,house8,house9});
				GameObject go;
				GameObject insGo = houses[Random.Range(0,9)]; 

				go = (GameObject)Instantiate(insGo, parent.transform.position + new Vector3( parent.transform.position.x + Random.Range(0,positionJitter), /*parent.transform.position.y*/insGo.transform.position.y,(parent.transform.position.z*2 + i * width) + Random.Range(0,zPositionJitter) ),parent.transform.rotation,parent.transform);
				go.transform.Rotate(0, Random.Range (-rotateJitter, rotateJitter), 0);
			}
		}
	}

	void OnGUI() {
		EditorGUILayout.Space ();
		parent = (GameObject)EditorGUI.ObjectField(new Rect (3, 3, position.width - 12, 20),"Find Parent", parent, typeof(GameObject));
		EditorGUILayout.Space ();

		house1 = (GameObject)EditorGUI.ObjectField (new Rect (3, 170, position.width - 12, 20), "Find GameObject", house1, typeof(GameObject));
		house2 = (GameObject)EditorGUI.ObjectField (new Rect (3, 190, position.width - 12, 20), "Find GameObject", house2, typeof(GameObject));
		house3 = (GameObject)EditorGUI.ObjectField (new Rect (3, 210, position.width - 12, 20), "Find GameObject", house3, typeof(GameObject));
		house4 = (GameObject)EditorGUI.ObjectField (new Rect (3, 230, position.width - 12, 20), "Find GameObject", house4, typeof(GameObject));
		house5 = (GameObject)EditorGUI.ObjectField (new Rect (3, 250, position.width - 12, 20), "Find GameObject", house5, typeof(GameObject));
		house6 = (GameObject)EditorGUI.ObjectField (new Rect (3, 270, position.width - 12, 20), "Find GameObject", house6, typeof(GameObject));
		house7 = (GameObject)EditorGUI.ObjectField (new Rect (3, 290, position.width - 12, 20), "Find GameObject", house7, typeof(GameObject));
		house8 = (GameObject)EditorGUI.ObjectField (new Rect (3, 310, position.width - 12, 20), "Find GameObject", house8, typeof(GameObject));
		house9 = (GameObject)EditorGUI.ObjectField (new Rect (3, 330, position.width - 12, 20), "Find GameObject", house9, typeof(GameObject));
		house0 = ((GameObject)EditorGUI.ObjectField (new Rect (3, 350, position.width - 12, 20), "Find GameObject", house0, typeof(GameObject)));
		EditorGUILayout.Space ();

			if (GUILayout.Button ("Generate Buildings")) {
				spawnBuilding ();
			}
	
		EditorGUILayout.Space ();
		positionJitter = EditorGUILayout.Slider ("Position Jitter",positionJitter,-10f, 10f);
		EditorGUILayout.Space ();
		zPositionJitter = EditorGUILayout.Slider ("Z Position Jitter",zPositionJitter,-10f, 10f);
		EditorGUILayout.Space ();
		rotateJitter = EditorGUILayout.Slider ("Rotaton Jitter",rotateJitter,-20f, 20f);
		EditorGUILayout.Space ();
		length = EditorGUILayout.IntSlider("Length",length,0, 500);
		EditorGUILayout.Space ();
		width = EditorGUILayout.Slider("Width of House",width,0, 100f);




	}

}
#endif