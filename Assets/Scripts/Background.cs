using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	public float scrollSpeed;
	private Vector2 savedOffset;

	void Start () {
		savedOffset = new Vector2 (Time.time * scrollSpeed, 0);
		GetComponent<Renderer> ().material.mainTextureOffset = savedOffset;
	}

	void Update () {
		if(GameObject.Find("Player") != null){
			float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
			savedOffset = new Vector2 (Time.time * scrollSpeed, 0);
			GetComponent<Renderer> ().material.mainTextureOffset = savedOffset;
		}
	}

	void OnDisable () {
		//renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
