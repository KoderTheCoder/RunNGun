using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {
	float leftBoundry;
	float rightBoundry;
	float platformWidth;
	float zombieSpeed;
	float timeSinceLastChange;
	float waitTime;
	bool flipSprite;
	bool moving;
	public bool dead;
	Rigidbody2D rb;
	Animator zombieAnimator;
	bool playedSound;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		zombieAnimator = GetComponent<Animator> ();
		zombieSpeed = 2;
		waitTime = 6;
		timeSinceLastChange = 0;
		moving = false;
		playedSound = false;
	}

	// Update is called once per frame
	void Update () {
		if(!dead){
			if(transform.position.x < leftBoundry || transform.position.x > rightBoundry){
				walk ();
				print (leftBoundry.ToString() + " " + rightBoundry.ToString());
			}
			if(timeSinceLastChange > waitTime){
				if(moving){
					if(Random.Range(0, 100) < 25){
						rb.velocity = new Vector2 (0, 0);
						moving = false;
						timeSinceLastChange = 0;
						zombieAnimator.SetBool ("moving", moving);
					}
				}else if(Random.Range(0, 100) < 50){
					if(transform.position.x < leftBoundry || transform.position.x > rightBoundry){
						walk ();
						//print (leftBoundry.ToString() + " " + rightBoundry.ToString());
					}else{
						walk ();
					}
					timeSinceLastChange = 0;
				}
			}else{
				timeSinceLastChange += Time.deltaTime;
			}
		}else if(!playedSound){
			GetComponent<AudioSource> ().Play ();
			playedSound = true;
		}
	}

	void OnCollisionEnter2D ( Collision2D other){
		if(other.gameObject.tag == "Platform"){
			platformWidth = other.gameObject.GetComponent<BoxCollider2D> ().size.x;
			leftBoundry = other.gameObject.GetComponent<Transform> ().position.x - platformWidth/2 + GetComponent<BoxCollider2D>().size.x;
			rightBoundry = leftBoundry + platformWidth - GetComponent<BoxCollider2D>().size.x*2;
		}
	}

	void walk(){
		moving = true;
		zombieSpeed = zombieSpeed * -1;
		if(zombieSpeed < 0){
			flipSprite = true;
		}else{
			flipSprite = false;
		}
		zombieAnimator.SetBool ("moving", moving);
		GetComponent<SpriteRenderer> ().flipX = flipSprite;
		rb.velocity = new Vector2(zombieSpeed, 0);
	}

	public void reset(){
		playedSound = false;
		zombieSpeed = 2;
		waitTime = 6;
		timeSinceLastChange = 0;
		moving = false;
		dead = false;
	}

}
