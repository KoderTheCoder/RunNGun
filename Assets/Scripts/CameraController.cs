using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController thePlayer;
    private Vector3 lastPlayerPosition;
    private float distanceToMove;
	public AudioSource bgm;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("Sound") == "off"){
			bgm.Stop ();
		}
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        lastPlayerPosition = thePlayer.transform.position;
    }
}
