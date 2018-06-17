using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public Text scoreText;
	public Text hiScoreText;

	public float scoreCount;
	public float hiScoreCount;

	public float pointsPerSecond;

	public bool scoreIncreasing;

	public bool shouldDouble;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("HighScore")){
			hiScoreCount = PlayerPrefs.GetFloat ("HighScore");
		}
		scoreIncreasing = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(scoreIncreasing){
			scoreCount += pointsPerSecond * Time.deltaTime;
            PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore")+ (int)(pointsPerSecond * Time.deltaTime));

        }

		if(scoreCount > hiScoreCount){
			hiScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HighScore", hiScoreCount);
            PlayerPrefs.SetInt("NewHighScore", 1);
        }



        scoreText.text = "Score: " + (int)scoreCount;
		hiScoreText.text = "High Score: " + (int)hiScoreCount;
	}

    public void AddToTechTrash()
    {
        PlayerPrefs.SetInt("TechTrash", PlayerPrefs.GetInt("TechTrash") + 50);
        print(PlayerPrefs.GetInt("TechTrash"));
        
    }

	public void AddScore(int pointsToAdd){
		if(shouldDouble){
			pointsToAdd = pointsToAdd * 2;
            
        }
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + (int)(pointsToAdd));
        scoreCount += pointsToAdd;
	}
}
