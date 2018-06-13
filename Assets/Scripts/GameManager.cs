using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject plasmaGun;
	public Transform platformGenerator;
	private Vector3 platformStartPoint;
	public PlayerController thePlayer;
	private Vector3 playerStartPoint;
	private ScoreManager theScoreManager;
	public int spawnLevel;
	private PlatformDestroyer[] platformList;

	public DeathMenu theDeathScreen;
	public bool powerupReset;
	// Use this for initialization
	void Start () {
        
		spawnLevel = 1;
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;

		theScoreManager = FindObjectOfType<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void restartGame(){
		spawnLevel = 1;
		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);
		theDeathScreen.gameObject.SetActive (true);
		//StartCoroutine ("RestartGameCo");
	}

	public void Reset (){
		spawnLevel = 1;
		powerupReset = true;
		theDeathScreen.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for(int i = 0; i < platformList.Length; ++i){
			platformList [i].gameObject.SetActive (false);
		}
        if (PlayerPrefs.GetInt("Elixir of Iron Equipped") == 1)
        {
            thePlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 0.27f, 0.27f);
            thePlayer.elixirPoints = 3;
            thePlayer.armourImage.sprite = thePlayer.shieldSprites[0];
            thePlayer.armourImage.gameObject.SetActive(true);
        }
        thePlayer.elixirPoints = 0;
        thePlayer.myAnimator.speed = 1;
		thePlayer.transform.position = playerStartPoint;
		thePlayer.dead = false;
		platformGenerator.position = platformStartPoint;
        thePlayer.timer.text = "5";
        thePlayer.timer.gameObject.SetActive(false);
		thePlayer.gameObject.SetActive (true);
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	} 

	/*public IEnumerator RestartGameCo(){
		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.5f);

		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for(int i = 0; i < platformList.Length; ++i){
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	}*/
}
