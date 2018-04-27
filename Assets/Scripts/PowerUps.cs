using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;
	public float powerupLength;


	private PowerupManager thePowerupManager;

	public Sprite[] powerupSprites;
	// Use this for initialization
	void Start () {
		thePowerupManager = FindObjectOfType<PowerupManager> ();
	}

	void Awake(){
		int powerupSelecter = Random.Range (0, 2);
		switch(powerupSelecter){
		case 0:
			doublePoints = true; 
			safeMode = false;
			break;
		case 1:
			safeMode = true;
			doublePoints = false;
			break;
		}

		GetComponent<SpriteRenderer> ().sprite = powerupSprites [powerupSelecter];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.name == "Player"){
			thePowerupManager.ActivatePowerup (doublePoints, safeMode, powerupLength);
		}
		gameObject.SetActive (false);
	}

}
