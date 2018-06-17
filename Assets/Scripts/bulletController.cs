using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {
	public GameManager theGameManager;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		theGameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		if(gameObject.name == "heroLaser" || gameObject.name == "heroLaser(Clone)"){
			rb.velocity = new Vector2 (25, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.name == "heroLaser" || gameObject.name == "heroLaser(Clone)"){
			rb.velocity = new Vector2 (25, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        print("Entered");
		if(gameObject.name == "heroLaser" || gameObject.name == "heroLaser(Clone)"){
			if (other.tag == "enemy")
			{
				print ("true");
				kill (other);
			}
            else if (other.CompareTag("BulletCatcher"))
            {
                print("hit bullet catcher");
                gameObject.SetActive(false);
            }
        }
        else {
			if (other.tag == "Player")
			{
				if(!other.GetComponent<PlayerController> ().dead){
                    if (other.GetComponent<PlayerController>().elixirPoints < 1)
                    {
                        theGameManager.restartGame();
                        other.GetComponent<PlayerController>().dead = true;
                        other.GetComponent<PlayerController>().deathSound.Play();
                    }
                    else
                    {
                        other.GetComponent<PlayerController>().elixirPoints -= 1;
                        other.GetComponent<PlayerController>().armourImage.sprite = other.GetComponent<PlayerController>().shieldSprites[3 - other.GetComponent<PlayerController>().elixirPoints];
                        other.GetComponent<PlayerController>().armourDamage.Play();
                        gameObject.SetActive(false);
                    }
                    
				}
			}
		}

	}

	private void kill(Collider2D other)
	{   

		if(other.gameObject.name == "Zombie(Clone)" || other.gameObject.name == "Zombie"){
			other.gameObject.GetComponent<ZombieController> ().dead = true;
		}
		other.gameObject.GetComponent<turretController> ().dead = true;
		other.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		other.gameObject.GetComponent<Animator> ().SetBool ("Dead", true);
		//Wait for 14 secs.
		gameObject.SetActive(false);
	}
}
