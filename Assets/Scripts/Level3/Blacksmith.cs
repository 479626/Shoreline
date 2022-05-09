using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    [Header("Exit Transport")]
    public VectorValue playerStorage;
    public Vector2 playerPos;

    [Header("Script Variables")]
    public PlayerStats stats;
    private float percentage, potionLevel;
    private string potionType = "No potion";
    private int currentPrice, currentItem, potionPrice, bootPrice, swordPrice;

    [Header("UI Elements")]
    public Animator uiAnim;
    public Slider slider;
    public Image icon;
    public GameObject buyMenu, buyButton, shoeButton;
    public Text itemName, itemDescription, upgradePercentage, buttonText;
    public Sprite sword, potions, boots;

    [Header("NPC")]
    public Animator blacksmithAnim;

    private void Awake()
    {
        shoeButton.GetComponent<Button>().Select();
        stats.discoverBlacksmith = true;
        buyMenu.SetActive(false);    
    }

    private void Update()
    {
        UpdateMenuValues();
    }

    private void UpdateMenuValues()
    {
        upgradePercentage.text = "Upgraded: " + percentage + "%";
        buttonText.text = "Buy for: " + currentPrice;
    }

    private void UpdateMenu(Sprite newImage, string newName, string newDescription, float sliderValue, int price)
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
        itemDescription.text = newDescription;
        itemName.text = newName;
        icon.sprite = newImage;
        buyMenu.SetActive(true);
    }

        #region Button Functions

    public void OnExit()
    {
        SoundManager.instance.DoorSound();
        playerStorage.initialValue = playerPos;

        switch (stats.currentLevel)
        {
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 3:
                SceneManager.LoadScene(8);
                break;
        }
    }

    public void OnPurchase()
    {
        if (stats.coins >= currentPrice)
        {
            stats.coins -= currentPrice;
            SoundManager.instance.PurchaseSound();
            blacksmithAnim.SetTrigger("Sale");

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
                    stats.damageBonus += 5;
                    OnBack();
                }
            }
            if (currentItem == 2)
            {
                if (stats.speedModifier < 2f)
                {
                    stats.speedModifier += 0.5f;
                    OnBack();
                }
            }
        }
        else
        {
            SoundManager.instance.FailSound();
            blacksmithAnim.SetTrigger("Fail");
            uiAnim.SetTrigger("Fail");
        }
        shoeButton.GetComponent<Button>().Select();
    }

    public void OnBack()
    {
        buyMenu.SetActive(false);
    }

    public void SwordUpgrade()
    {
        currentItem = 1;
        string swordDescription = "Gain a bonus +5 damage per attack";

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
                stats.swordType = "Sharpened Rusty Cutlass";
                break;
            case 10:
                stats.swordType = "Iron Cutlass";
                break;
            case 15:
                stats.swordType = "Sharpened Iron Cutlass";
                break;
            case 20:
                stats.swordType = "Damascus Steel Cutlass";
                break;
        }

        float swordLevel = stats.damageBonus / 20f;
        string swordName = stats.swordType;

        UpdateMenu(sword, swordName, swordDescription, swordLevel, swordPrice);
    }

    public void CoinPotion()
    {
        currentItem = 0;
        string potionDescription = "Gain a bonus silver coin every item drop";

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

        UpdateMenu(potions, potionType, potionDescription, potionLevel, potionPrice);
    }

    public void ShoeUpgrade()
    {
        currentItem = 2;
        float currentSpeed = stats.speedModifier;
        string bootDescription = "Gain a +0.5 speed buff";

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

        UpdateMenu(boots, bootType, bootDescription, speedLevel, bootPrice);
    }

    #endregion
}
