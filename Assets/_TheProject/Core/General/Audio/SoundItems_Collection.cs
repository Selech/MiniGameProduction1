using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
[CreateAssetMenu(fileName = "SoundItem_Collection",menuName = "SoundItem_Collection")]
public class SoundItems_Collection : ScriptableObject 
{
	
	public List<Sound_Item> soundsCollection = new List<Sound_Item>();
}
