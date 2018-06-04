using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicController : MonoBehaviour {
    public GameObject[] lockedScreens;
	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetString("Sound") == "off")
        {
            GetComponent<AudioSource>().Stop();
        }
        for (int i = 0; i < lockedScreens.Length; ++i)
        {
            if (PlayerPrefs.GetInt("Chapter" + (i+1).ToString()) == 1)
            {
                lockedScreens[i].SetActive(false);
            }
        }
	}
}
