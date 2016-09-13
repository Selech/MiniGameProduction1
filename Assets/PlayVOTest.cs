using UnityEngine;
using System.Collections;

public class PlayVOTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			//play sound with index one in the DB. chooses language based on isEnglish
			GameManager.Instance.PlaySoundVO (1);
		} else if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			GameManager.Instance.PlaySoundVO (2);
		}
		else if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			GameManager.Instance.PlaySoundVO (3);
		}
		else if(Input.GetKeyDown(KeyCode.Keypad4))
		{
			GameManager.Instance.PlaySoundVO (4);
		}

		if(Input.GetButtonDown("Jump"))
		{
			GameManager.Instance._soundEventsContainer.isEnglish = !GameManager.Instance._soundEventsContainer.isEnglish;
		}
	}

//	//generic method for playing sound
//	public void PlaySoundVO(int index)
//	{
//
//		foreach (Sound_Item v in _soundEventsContainer._voiceOver_Collection) 
//		{
//			if (GameManager.Instance._soundEventsContainer.isEnglish) {
//				if (v.soundIndex == index && v._Language == Language.English) {
//					PlaySound (v.soundEventName);
//					break;
//				}
//			} else {
//				if (v.soundIndex == index && v._Language == Language.Danish) {
//					PlaySound (v.soundEventName);
//					break;
//				}
//			}
//
//		}
//	}

}
