using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemsMenu : MonoBehaviour {

    int[] prices;
    public Sprite[] itemImages;
    public Image[] imageHolders;
    public GameObject lockSprite;
    public Text price;
    public Text itemTitle;
    public Text totalTechTrash;
    public Text unlockStatus;
    public AudioSource bgm;
    public AudioSource purchased;
    public AudioSource gunEquipped;
    public AudioSource elixirEquipped;
    public AudioSource suitEquipped;
    string currentEquipped;
    int currentItem;

	// Use this for initialization
	void Start () {

        //PlayerPrefs.SetInt("TechTrash", 100000);
        //PlayerPrefs.SetInt("Plasma Gun", 0);
        //PlayerPrefs.SetInt("Elixir of Iron", 0);
        if (PlayerPrefs.GetString("Sound") == "off")
        {
            bgm.Stop();
        }
        currentItem = 0;
		prices = new int[] { 30000, 50000, 100000};
        imageHolders[0].sprite = itemImages[0];
        totalTechTrash.text = PlayerPrefs.GetInt("TechTrash").ToString();
        itemTitle.text = itemImages[0].name;
        if(PlayerPrefs.GetInt(itemImages[0].name) == 1)
        {
            lockSprite.SetActive(false);
            if(PlayerPrefs.GetInt(itemImages[0].name + " Equipped") == 1)
            {
                unlockStatus.text = "Unequip";
            }
            else
            {
                unlockStatus.text = "Equip";
            }
        }
        else
        {
            unlockStatus.text = "Unlock";
        }
        
    }

    public void unlockItem()
    {
        if (PlayerPrefs.GetInt("TechTrash") >= prices[currentItem] && PlayerPrefs.GetInt(itemImages[currentItem].name) == 0)
        {
            PlayerPrefs.SetInt(itemImages[currentItem].name, 1);
            PlayerPrefs.SetInt("TechTrash", PlayerPrefs.GetInt("TechTrash") - prices[currentItem]);
            totalTechTrash.text = PlayerPrefs.GetInt("TechTrash").ToString();
            purchased.Play();
            lockSprite.SetActive(false);
            unlockStatus.text = "Equip";
        }
        else if(PlayerPrefs.GetInt(itemImages[currentItem].name) == 1)
        {
            if(PlayerPrefs.GetInt(itemImages[currentItem].name + " Equipped") == 1)
            {
                currentEquipped = "none";
                unlockStatus.text = "Equip";
                PlayerPrefs.SetInt(itemImages[currentItem].name + " Equipped", 0);
            }
            else
            {
                unlockStatus.text = "Unequip";
                PlayerPrefs.SetInt(currentEquipped + " Equipped", 0);
                PlayerPrefs.SetInt(itemImages[currentItem].name + " Equipped", 1);
                currentEquipped = itemImages[currentItem].name;
                print(currentEquipped);
                if(itemImages[currentItem].name == "Plasma Gun")
                {
                    gunEquipped.Play();
                }else if(itemImages[currentItem].name == "Elixir of Iron")
                {
                    elixirEquipped.Play();
                }else if (itemImages[currentItem].name == "Super Suit")
                {
                    suitEquipped.Play();
                }
                
            }
        }

        if(PlayerPrefs.GetInt("TechTrash") < prices[currentItem] && PlayerPrefs.GetInt(itemImages[currentItem].name) == 0)
        {
            StartCoroutine(ColorFlash(0.3f, totalTechTrash.color));
            totalTechTrash.color = UnityEngine.Color.red;
            

        }
    }

    public void navRight()
    {
        if(currentItem != 2)
        {
            currentItem++;
            if(currentItem != 2)
            {
                imageHolders[1].sprite = itemImages[2];
                imageHolders[2].sprite = itemImages[0];
            }
            else
            {
                imageHolders[1].sprite = itemImages[0];
                imageHolders[2].sprite = itemImages[1];
            }
        }
        else
        {
            currentItem = 0;
            imageHolders[1].sprite = itemImages[1];
            imageHolders[2].sprite = itemImages[2];
        }

        imageHolders[0].sprite = itemImages[currentItem];
        price.text = prices[currentItem].ToString();
        itemTitle.text = itemImages[currentItem].name;
        if (PlayerPrefs.GetInt(itemImages[currentItem].name) == 1)
        {
            lockSprite.SetActive(false);
            if (PlayerPrefs.GetInt(itemImages[currentItem].name + " Equipped") == 1)
            {
                unlockStatus.text = "Unequip";
            }
            else
            {
                unlockStatus.text = "Equip";
            }
        }
        else
        {
            unlockStatus.text = "Unlock";
            lockSprite.SetActive(true);
        }
    }

    public void navLeft()
    {
        if (currentItem != 0)
        {
            currentItem--;
            if (currentItem != 0)
            {
                imageHolders[1].sprite = itemImages[2];
                imageHolders[2].sprite = itemImages[0];
            }
            else
            {
                imageHolders[1].sprite = itemImages[1];
                imageHolders[2].sprite = itemImages[2];
            }
        }
        else
        {
            currentItem = 2;
            imageHolders[1].sprite = itemImages[0];
            imageHolders[2].sprite = itemImages[1];
        }

        imageHolders[0].sprite = itemImages[currentItem];
        price.text = prices[currentItem].ToString();
        itemTitle.text = itemImages[currentItem].name;
        if (PlayerPrefs.GetInt(itemImages[currentItem].name) == 1)
        {
            lockSprite.SetActive(false);
            if (PlayerPrefs.GetInt(itemImages[currentItem].name + " Equipped") == 1)
            {
                unlockStatus.text = "Unequip";
            }
            else
            {
                unlockStatus.text = "Equip";
            }
        }
        else
        {
            unlockStatus.text = "Unlock";
            lockSprite.SetActive(true);
        }
    }
    IEnumerator ColorFlash(float time, UnityEngine.Color color)
    {
        yield return new WaitForSeconds(time);
        print("Done");
        totalTechTrash.color = color;
    }

}
