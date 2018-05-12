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

	public GameManager theGameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;

	// Use this for initialization
	void Start () {
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
				speedMilestoneCount += speedIncreaseMilestone;
				moveSpeed = moveSpeed * speedMultiplier;
				speedIncreaseMilestone = speedIncreaseMilestone*speedMultiplier;
			}

			if (Input.GetMouseButtonDown(0) && (grounded || canDoubleJump))
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;
				if(!grounded && canDoubleJump){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce*1.5f);
					stoppedJumping = false;
					canDoubleJump = false;
				}
				jumpSound.Play ();
			}

			if(Input.GetKeyUp(KeyCode.Mouse0)){
				jumpTimeCounter = 0;
				stoppedJumping = true;
			}
			if(grounded){
				jumpTimeCounter = jumpTime;
				canDoubleJump = true;
			}
			if(Input.GetKey (KeyCode.Mouse0) && !stoppedJumping){
				if(jumpTimeCounter > 0){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
					jumpTimeCounter -= Time.deltaTime;
				}
			}
        
        myAnimator.SetFloat("Speed", moveSpeed);
		myAnimator.SetBool("Grounded", grounded);
		myAnimator.SetFloat("Jump Time", jumpTime);
		myAnimator.SetBool("Dead", dead);
    }

	void OnCollisionEnter2D ( Collision2D other){
		if(other.gameObject.tag == "killbox"){
			theGameManager.restartGame ();
			moveSpeed = moveSpeedStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
			speedMilestoneCount = speedMilestoneCountStore;
			dead = true;
			deathSound.Play ();
		}
	}
}
