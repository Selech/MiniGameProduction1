using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ItemsCollection_Carriable",menuName = "ItemsCollection_Carriable")]
public class ItemsCollection_Carriable : ScriptableObject {
	[SerializeField]
	public List<Item_Carriable> itemsCollection = new List<Item_Carriable>();
}
