using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.aspect = 480f / 800f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
