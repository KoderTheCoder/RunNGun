using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

	public Transform generationPoint;
	public float distanceBetween;

	public float distanceBetweenMin;
	public float distanceBetweenMax;

	//public GameObject [] thePlatforms;
	private int platformSelector;
	private float [] platformWidths;

	public ObjectPooler [] theObjectPools;

	private float minHeight;
	public Transform maxHeightPoint;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;

	private CoinGenerator theCoinGenerator;
	public float randomCoinThreshhold;

	public float randomSpikeThreshold;
	public ObjectPooler theSpikePool;

	public float powerupHeight;
	public ObjectPooler powerupPool;
	public float powerupThreshold;

	// Use this for initialization
	void Start () {
		theCoinGenerator = FindObjectOfType <CoinGenerator>();
		platformWidths = new float[theObjectPools.Length];

		for(int i = 0; i < theObjectPools.Length; ++i){
			platformWidths [i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		

		if (transform.position.x < generationPoint.position.x) {

			platformSelector = Random.Range (0, theObjectPools.Length);

			heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);

			if (heightChange > maxHeight) {
				heightChange = maxHeight;
			} else if(heightChange < minHeight){
				heightChange = minHeight;
			}
				

			distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);
			
			transform.position = new Vector3 (transform.position.x + platformWidths[platformSelector]/2 + distanceBetween, heightChange, transform.position.z);

			//Instantiate (thePlatforms[platformSelector], transform.position, transform.rotation);

			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject ();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);


			if(Random.Range(0, 100) < randomCoinThreshhold){
				theCoinGenerator.SpawnCoins (new Vector3(transform.position.x, transform.position.y+1f,transform.position.z));
			}
			if(Random.Range(0, 100) < randomSpikeThreshold){
				GameObject newSpike = theSpikePool.GetPooledObject ();

				float spikeXPosition = Random.Range (-platformWidths [platformSelector] / 2 + 1f, platformWidths [platformSelector] / 2 - 1f);

				Vector3 spikePosition = new Vector3 (spikeXPosition, 0.5f, 0f);

				newSpike.transform.position = transform.position + spikePosition;
				newSpike.transform.rotation = transform.rotation;
				newSpike.SetActive(true);
			}

			if(Random.Range(0, 100) < powerupThreshold){
				GameObject newPowerup = powerupPool.GetPooledObject ();
				newPowerup.transform.position = transform.position + new Vector3(distanceBetween/2f, Random.Range(1, powerupHeight), 0);
				newPowerup.SetActive (true);
			}

			transform.position = new Vector3 (transform.position.x + platformWidths[platformSelector]/2, transform.position.y, transform.position.z);
		}
	}
}
