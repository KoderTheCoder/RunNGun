using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;
	public Image mute;
	public Sprite soundImage;
	public Sprite noSoundImage;
	public AudioSource bgm;


	void Start(){
		if(PlayerPrefs.GetString("Sound")=="off"){
			mute.sprite = noSoundImage;
			bgm.Stop();
		}
	}
	public void PlayGame (){
		Application.LoadLevel (playGameLevel);
	}

	public void muteSound(){
		if(PlayerPrefs.GetString("Sound") == "off"){
			PlayerPrefs.SetString ("Sound", "on");
			mute.sprite = soundImage;
			bgm.Play ();
		}else{
			PlayerPrefs.SetString ("Sound", "off");
			mute.sprite = noSoundImage;
			bgm.Pause();
		}
	}

	public void QuitGame (){
		Application.Quit ();
	}
}
