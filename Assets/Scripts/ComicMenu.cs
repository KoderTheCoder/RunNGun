using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ComicMenu : MonoBehaviour {
    public GameObject storyBoard;
    public GameObject backButton;
    public Image comicPage;
    public Sprite[] spr;

	public void GoBack()
    {
        print("adsad");
        PlayerPrefs.SetInt("Chapter1", 1);
        SceneManager.LoadScene("MainMenu");
        
    }

    public void NextPage()
    {
        comicPage.sprite = GameManager.chapterSprites[1];
    }

    public void PreviousPage()
    {
        comicPage.sprite = GameManager.chapterSprites[0];
    }

    public void ComicExit()
    {
        backButton.SetActive(true);
        storyBoard.SetActive(false);
    }

    public void LoadChapter()
    {
        if(PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            backButton.SetActive(false);
            storyBoard.SetActive(true);
            print("adsad");
            comicPage.sprite = spr[0];
            GameManager.chapterSprites = spr;
        }
    }
}
