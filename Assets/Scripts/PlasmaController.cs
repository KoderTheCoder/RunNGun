using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaController : MonoBehaviour {
    public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(15, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "laserShot(Clone)")
        {
            col.gameObject.SetActive(false);
        }
    }
}
