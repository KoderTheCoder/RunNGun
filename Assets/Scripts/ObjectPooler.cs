using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {
	
	public GameObject pooledObject;

	public int pooledAmount;

	public List<GameObject> pooledObjects;

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject> ();

		for (int i = 0; i < pooledAmount; ++i) {
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
	}
	
	public GameObject GetPooledObject(){
		foreach(GameObject obj in pooledObjects){
			if(!obj.activeInHierarchy){
				return obj;
			}
		}
		GameObject obje = (GameObject)Instantiate (pooledObject);
		obje.SetActive (false);
		pooledObjects.Add (obje);
		return obje;
	}
}
