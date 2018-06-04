using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool dead;
    public float moveSpeed;
	private float moveSpeedStore;
	public float speedMultiplier;

	public float speedIncreaseMilestone;
	private float speedIncreaseMilestoneStore;

	private float speedMilestoneCount;
	private float speedMilestoneCountStore;


    public float jumpForce;
	public float jumpTime;
	private float jumpTimeCounter;
    public bool grounded;
	private bool stoppedJumping;
	private bool canDoubleJump;

    public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

    //private Collider2D myCollider;

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;
    public Animation anim;

    public GameManager theGameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;

	public GameObject jetpack;

	public ObjectPooler laserPool;

	float timeSinceLastShot;
	public float shotFrequency;

	// Use this for initialization
	void Start () {
		timeSinceLastShot = shotFrequency;
		dead = false;
        grounded = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        //myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedIncreaseMilestone;
		moveSpeedStore = moveSpeed;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		speedMilestoneCountStore = speedMilestoneCount;
	}
	
	// Update is called once per frame
	void Update () {
			myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
			//grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

			grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

			if(transform.position.x > speedMilestoneCount){
				theGameManager.spawnLevel++;
				speedMilestoneCount += speedIncreaseMilestone;
				moveSpeed = moveSpeed * speedMultiplier;
				speedIncreaseMilestone = speedIncreaseMilestone*speedMultiplier;
                if(myAnimator.speed < 2)
                {
                    myAnimator.speed += 0.15f;
                }
                
			}
		if(((Input.mousePosition.x > 200 || Input.mousePosition.y > 200) || Input.touches.Length > 1) && (Input.mousePosition.x > 200 || Input.mousePosition.y < 900)){
			if (Input.GetMouseButtonDown(0) && (grounded || canDoubleJump))
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;
				if(!grounded && canDoubleJump){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce*1.4f);
					stoppedJumping = false;
					canDoubleJump = false;
				}
				jumpSound.Play ();
			}else if(Input.GetKey (KeyCode.Mouse0) && !grounded && myRigidbody.velocity.y < 0){
				myRigidbody.AddForce (new Vector2 (0, 30));
				jetpack.SetActive (true);
			}

			if(Input.GetKeyUp(KeyCode.Mouse0)){
				jumpTimeCounter = 0;
				stoppedJumping = true;
				jetpack.SetActive (false);
			}
			if(grounded){
				jumpTimeCounter = jumpTime;
				canDoubleJump = true;
				jetpack.SetActive (false);
			}
			if(Input.GetKey (KeyCode.Mouse0) && !stoppedJumping){
				
				if(jumpTimeCounter > 0){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
					jumpTimeCounter -= Time.deltaTime;
					jetpack.SetActive (true);
				}
			}
		}

		if(Input.GetKey (KeyCode.Space)){
			shoot ();
		}
		timeSinceLastShot += Time.deltaTime;
        myAnimator.SetFloat("Speed", moveSpeed);
		myAnimator.SetBool("Grounded", grounded);
		myAnimator.SetBool("Dead", dead);

    }

	void OnCollisionEnter2D ( Collision2D other){
		if(other.gameObject.tag == "killbox" || other.gameObject.tag == "enemy"){
			theGameManager.restartGame ();
			moveSpeed = moveSpeedStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
			speedMilestoneCount = speedMilestoneCountStore;
			dead = true;
			deathSound.Play ();
            int unlock = 10000;
            for (int i = 0; i < 8; ++i)
            {
                if (PlayerPrefs.GetInt("TotalScore") > unlock)
                {
                    PlayerPrefs.SetInt("Chapter" + (i + 1).ToString(), 1);
                    unlock = (int)((float)unlock * 1.5f);
                }
                else
                {
                    break;
                }
            }
        }
	}

	public void shoot(){
		if (timeSinceLastShot >= shotFrequency) {
			GameObject laser = laserPool.GetPooledObject ();
			laser.transform.position = new Vector3 (transform.position.x+1.5f, transform.position.y + 0.5f, transform.position.z);
			laser.transform.rotation = transform.rotation;
			laser.SetActive (true);
			GetComponent<AudioSource> ().Play ();
			timeSinceLastShot = 0;
		}
	}
}
