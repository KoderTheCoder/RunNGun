using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretController : MonoBehaviour {
	bool dead;
	float shotFrequency;
	public ObjectPooler theLaserPool;
	float timeSinceLastShot;
	AudioSource laserShot;
	// Use this for initialization
	void Start () {
		dead = false;
		shotFrequency = 2;
		laserShot = GetComponent<AudioSource> ();
		theLaserPool = GameObject.Find ("LaserPool").GetComponent<ObjectPooler>();
	}

	void Awake(){
		dead = false;
		shotFrequency = 2;
		theLaserPool = GameObject.Find ("LaserPool").GetComponent<ObjectPooler>();
		laserShot = GetComponent<AudioSource> ();
		shoot ();
	}



	
	// Update is called once per frame
	void Update () {
		if(!dead){
			if(timeSinceLastShot >= shotFrequency){
				shoot ();
				timeSinceLastShot = 0;
			}else{
				timeSinceLastShot += Time.deltaTime;
			}

		}
	}

	void shoot(){
		GameObject laser = theLaserPool.GetPooledObject ();
		laser.transform.position = transform.position - new Vector3 (GetComponent<Renderer>().bounds.size.x, -GetComponent<Renderer>().bounds.size.y/2);
		Rigidbody2D rb = laser.GetComponent<Rigidbody2D> ();
		laser.SetActive (true);
		laserShot.Play ();
		rb.velocity = new Vector3 (-5, 0, 0);
	}
}
