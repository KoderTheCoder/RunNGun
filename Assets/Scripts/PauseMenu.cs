using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public string mainMenuLevel;

	public GameObject pauseMenu;

	public void PauseGame () {
		Time.timeScale = 0f;
		pauseMenu.SetActive (true);
	}

	public void ResumeGame () {
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
	}

	public void RestartGame () {
		Time.timeScale = 1f;
		FindObjectOfType<GameManager> ().Reset ();
		pauseMenu.SetActive (false);
	}

	public void QuitToMain(){
		Time.timeScale = 1f;
		Application.LoadLevel (mainMenuLevel);
		pauseMenu.SetActive (false);
	}
}
