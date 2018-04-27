using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

	private bool doublePoints;
	private bool safeMode;
	private bool powerupActive;

	public float powerupLengthCounter;

	private ScoreManager theScoreManager;
	private GameManager theGameManager;
	private PlatformCreator thePlatformCreator;

	public float normalPointsPerSecond;
	public float spikeRate;

	private PlatformDestroyer[] spikeList;

	// Use this for initialization
	void Start () {
		theGameManager = FindObjectOfType<GameManager> ();
		theScoreManager = FindObjectOfType<ScoreManager> ();
		thePlatformCreator = FindObjectOfType<PlatformCreator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(powerupActive){
			powerupLengthCounter -= Time.deltaTime;

			if(theGameManager.powerupReset){
				powerupLengthCounter = 0;
				theGameManager.powerupReset = false;
			}

			if(doublePoints){
				theScoreManager.shouldDouble = true;
			}

			if(safeMode){
				thePlatformCreator.randomSpikeThreshold = 0;
			}

			if(powerupLengthCounter <= 0){
				theScoreManager.pointsPerSecond = normalPointsPerSecond;
				thePlatformCreator.randomSpikeThreshold = spikeRate;
				theScoreManager.shouldDouble = false;
				powerupActive = false;
			}
		}
	}

	public void ActivatePowerup(bool points, bool safe, float time){
		doublePoints = points;
		safeMode = safe;
		powerupLengthCounter = time;

		normalPointsPerSecond = theScoreManager.pointsPerSecond;
		spikeRate = thePlatformCreator.randomSpikeThreshold;

		if(safeMode){
			spikeList = FindObjectsOfType<PlatformDestroyer> ();
			for(int i = 0; i < spikeList.Length; ++i){
				if(spikeList[i].gameObject.name.Contains("spikes")){
					spikeList [i].gameObject.SetActive (false);
				}
			}
		}

		powerupActive = true;
	}
}
