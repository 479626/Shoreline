using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    [Header("Exit Transport")]
    public VectorValue playerStorage;
    public int scene;
    public Vector2 playerPos;

    [Header("Script Variables")]
    public PlayerStats stats;
    private float percentage, potionLevel;
    private string potionType = "No potion";
    private int currentPrice, currentItem, potionPrice, bootPrice, swordPrice;

    [Header("UI Elements")]
    public Slider slider;
    public Image icon;
    public GameObject buyMenu, buyButton;
    public Text itemName, upgradePercentage, buttonText;
    public Sprite sword, potions, boots;

    void Awake()
    {
        buyMenu.SetActive(false);    
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            stats.coins++;
            Debug.Log("Added 1 coin. You have: " + stats.coins);
        }

        upgradePercentage.text = "Power level " + percentage.ToString() + "%";
        buttonText.text = "Buy for: " + currentPrice.ToString();
    }

    private void UpdateMenu(Sprite newImage, string newName, float sliderValue, int price)
    {
        buyButton.SetActive(true);

        if (stats.speedModifier == 2f && currentItem == 2)
        {
            buyButton.SetActive(false);
        }
        if (stats.damageBonus == 20 && currentItem == 1)
        {
            buyButton.SetActive(false);
        }
        if (stats.greedy && currentItem == 0)
        {
            buyButton.SetActive(false);
        }

        currentPrice = price;
        slider.value = sliderValue;
        percentage = sliderValue * 100F;
        itemName.text = newName;
        icon.sprite = newImage;
        buyMenu.SetActive(true);
    }

    #region Button Functions

    public void OnExit()
    {
        playerStorage.initialValue = playerPos;
        SceneManager.LoadScene(scene);
    }

    public void OnPurchase()
    {
        if (stats.coins >= currentPrice)
        {
            stats.coins = stats.coins - currentPrice;
            if (currentItem == 0)
            {
                if (!stats.greedy)
                {
                    stats.greedy = true;
                    OnBack();
                }
            }
            if (currentItem == 1)
            {
                if (stats.damageBonus < 20)
                {
                    stats.damageBonus = stats.damageBonus + 5;
                    OnBack();
                }
            }
            if (currentItem == 2)
            {
                if (stats.speedModifier < 2f)
                {
                    stats.speedModifier = stats.speedModifier + 0.5f;
                    OnBack();
                }
            }
        }
    }

    public void OnBack()
    {
        buyMenu.SetActive(false);
    }

    public void SwordUpgrade()
    {
        currentItem = 1;

        if (stats.damageBonus >= 20)
        {
            swordPrice = 0;
        }
        else
        {
            swordPrice = stats.damageBonus + 5;
        }

        switch (stats.damageBonus)
        {
            case 5:
                stats.swordType = "Sharpened Rusty Rapier";
                break;
            case 10:
                stats.swordType = "Iron Rapier";
                break;
            case 15:
                stats.swordType = "Sharpened Iron Rapier";
                break;
            case 20:
                stats.swordType = "Damascus Steel Rapier";
                break;
        }

        float swordLevel = stats.damageBonus / 20f;
        string swordName = stats.swordType;

        UpdateMenu(sword, swordName, swordLevel, swordPrice);
    }

    public void CoinPotion()
    {
        currentItem = 0;

        if (stats.greedy)
        {
            potionPrice = 0;
            potionLevel = 1f;
            potionType = "Potion of Greed";
        }
        else
        {
            potionPrice = 15;
        }

        UpdateMenu(potions, potionType, potionLevel, potionPrice);
    }

    public void ShoeUpgrade()
    {
        currentItem = 2;
        float currentSpeed = stats.speedModifier;

        if (currentSpeed == 2f)
        {
            bootPrice = 0;
        }
        else
        {
            bootPrice = (int)currentSpeed + 5;
        }

        switch (stats.speedModifier)
        {
            case 0.5f:
                stats.bootType = "New Running Shoes";
                break;
            case 1f:
                stats.bootType = "Beginner Running Shoes";
                break;
            case 1.5f:
                stats.bootType = "Novice Running Shoes";
                break;
            case 2f:
                stats.bootType = "Master Running Shoes";
                break;
        }

        float speedLevel = stats.speedModifier / 2f;
        string bootType = stats.bootType;

        UpdateMenu(boots, bootType, speedLevel, bootPrice);
    }

    #endregion
}
