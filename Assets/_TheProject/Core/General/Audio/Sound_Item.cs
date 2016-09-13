using UnityEngine;
using System.Collections;

public enum Language{
	English,
	Danish
}

[System.Serializable]
[CreateAssetMenu(fileName = "SoundItem",menuName = "SoundItem")]
public class Sound_Item : ScriptableObject 
{
	public int soundIndex;
	public string soundEventName;
	public Language _Language = Language.English;
	[TextArea(2,5)]
	public string text;
}
