using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "SoundItem",menuName = "SoundItem")]
public class Sound_Item : ScriptableObject 
{
	public int soundIndex;
	public string soundEventName;
	public float soundDuration;
}
