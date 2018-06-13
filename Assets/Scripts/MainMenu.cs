using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;
	public string storyScene;
	public Image mute;
	public Sprite soundImage;
	public Sprite noSoundImage;
	public AudioSource bgm;


	void Start(){
		if(PlayerPrefs.GetString("Sound")=="off"){
			mute.sprite = noSoundImage;
			bgm.Stop();
		}
        for (int i = 0; i < 8; ++i)
        {
            if (!PlayerPrefs.HasKey("Chapter"+(i+1)))
            {
                PlayerPrefs.SetInt("Chapter" + (i + 1), 0);
            }
        }
        if (!PlayerPrefs.HasKey("Elixir of Iron"))
        {
            PlayerPrefs.SetInt("Elixir of Iron", 0);
            PlayerPrefs.SetInt("Elixir of Iron Equipped", 0);
        }
        if (!PlayerPrefs.HasKey("Plasma Gun"))
        {
            PlayerPrefs.SetInt("Plasma Gun", 0);
            PlayerPrefs.SetInt("Plasma Gun Equipped", 0);
        }
        if (!PlayerPrefs.HasKey("Super Suit"))
        {
            PlayerPrefs.SetInt("Super Suit", 0);
            PlayerPrefs.SetInt("Super Suit Equipped", 0);
        }

        if (!PlayerPrefs.HasKey("TotalScore"))
        {
            PlayerPrefs.SetInt("TotalScore", 0);
        }

        if (!PlayerPrefs.HasKey("TechTrash"))
        {
            PlayerPrefs.SetInt("TechTrash", 0);
        }

    }
	public void PlayGame (){
		Application.LoadLevel (playGameLevel);
	}

	public void Story(){
		Application.LoadLevel (storyScene);
	}

    public void Inventory()
    {
        Application.LoadLevel("Inventory");
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
