using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "CarriableItem",menuName = "CarriableItem")]
public class Item_Carriable : ScriptableObject {
	public string itemName;
	public float carriableMass=1;
	public float carriableEffectFactor = 1;
	public int scoreFactor = 2;
	public GameObject carriablePrefab;
}
