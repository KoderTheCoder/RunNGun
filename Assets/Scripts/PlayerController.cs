using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Animator myAnimator;
    public Animation anim;

    public GameManager theGameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;
    public AudioSource plasmaShot;
    public AudioSource armourDamage;
	public GameObject jetpack;

	public ObjectPooler laserPool;
    public ObjectPooler plasmaPool;

    float timeSinceLastShot;
    float timeSinceLastPlasmaShot;
    public float shotFrequency;

    public Text timer;
    public GameObject plasmaGun;

    public int elixirPoints = 0;
    public Image armourImage;
    public Sprite[] shieldSprites;


	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("Plasma Gun Equipped") == 1)
        {
            plasmaGun.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Elixir of Iron Equipped") == 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.27f, 0.27f);
            elixirPoints = 3;
            armourImage.gameObject.SetActive(true);
        }
        timeSinceLastShot = shotFrequency;
        timeSinceLastPlasmaShot = 5;
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
        if (((Input.mousePosition.x < 1800 || Input.mousePosition.y > 180) || Input.touchCount > 1) && (Input.mousePosition.x > 200 || Input.mousePosition.y < 900)){
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
        timeSinceLastPlasmaShot += Time.deltaTime;
        myAnimator.SetFloat("Speed", moveSpeed);
		myAnimator.SetBool("Grounded", grounded);
		myAnimator.SetBool("Dead", dead);

    }

	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.tag == "killbox" || other.gameObject.tag == "enemy"){
            if (elixirPoints < 1 || other.gameObject.name == "Catcher")
            {
                theGameManager.restartGame();
                moveSpeed = moveSpeedStore;
                speedIncreaseMilestone = speedIncreaseMilestoneStore;
                speedMilestoneCount = speedMilestoneCountStore;
                dead = true;
                deathSound.Play();
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
            }else
            {
                kill(other);
                elixirPoints -= 1;
                GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.b + 0.24f, GetComponent<SpriteRenderer>().color.g + 0.24f);
                armourImage.sprite = shieldSprites[3 - elixirPoints];
                print(elixirPoints.ToString());
                armourDamage.Play();
            }
        }
	}

    public void shootPlasma()
    {
        if (timeSinceLastPlasmaShot >= 5)
        {
            GameObject plasma = plasmaPool.GetPooledObject();
            plasma.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.5f, transform.position.z);
            plasma.transform.rotation = Quaternion.Euler(0, 0, 90);
            plasma.SetActive(true);
            plasmaShot.Play();
            timeSinceLastPlasmaShot = 0;
            timer.gameObject.SetActive(true);
            StartCoroutine(TimerCountdown());
        }

    }

    private void kill(Collision2D other)
    {

        if (other.gameObject.name == "Zombie(Clone)" || other.gameObject.name == "Zombie")
        {
            other.gameObject.GetComponent<ZombieController>().dead = true;
        }
        other.gameObject.GetComponent<turretController>().dead = true;
        other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        other.gameObject.GetComponent<Animator>().SetBool("Dead", true);
        //Wait for 14 secs.
        //gameObject.SetActive(false);
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

    IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(1);
        timer.text = (int.Parse(timer.text) - 1).ToString();
        if (int.Parse(timer.text) > 0)
        {
            StartCoroutine(TimerCountdown());
        }
        else
        {
            timer.text = "5";
            timer.gameObject.SetActive(false);
        }
    }
}
