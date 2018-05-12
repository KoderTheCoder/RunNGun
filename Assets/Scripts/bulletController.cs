using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {
	public GameManager theGameManager;
	// Use this for initialization
	void Start () {
		theGameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if(!other.GetComponent<PlayerController> ().dead){
				theGameManager.restartGame ();
				other.GetComponent<PlayerController> ().dead = true;
				other.GetComponent<PlayerController> ().deathSound.Play();
			}
		}
	} 
}
