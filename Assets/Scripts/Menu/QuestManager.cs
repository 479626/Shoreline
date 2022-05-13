using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    [Header("Variables")]
    public QuestProgress progress;
    public InteractionCounter counter;
    public PlayerStats stats;
    public GameObject questMenu, levelOneButton, levelOneQuests, levelTwoQuests, levelThreeQuests, levelFourQuests;
    public static bool gamePaused, quests;
    private bool oneLevelOne, twoLevelOne, oneLevelTwo, oneLevelThree, twoLevelThree, threeLevelThree, fourLevelThree, oneLevelFour, twoLevelFour;

    [Header("UI Elements")]
    public Button startButton;
    public List<Text> levelOneRewardAmounts = new List<Text>();
    public List<Slider> levelOneSliders = new List<Slider>();
    public List<Text> levelTwoRewardAmounts = new List<Text>();
    public List<Slider> levelTwoSliders = new List<Slider>();
    public List<Text> levelThreeRewardAmounts = new List<Text>();
    public List<Slider> levelThreeSliders = new List<Slider>();
    public List<Text> levelFourRewardAmounts = new List<Text>();
    public List<Slider> levelFourSliders = new List<Slider>();

    [Header("Quest Award")]
    public Animator anim;
    public Text questText;
    public Text rewardText;

    void CheckForProgress()
    {
        // Level one quests
        levelOneSliders[0].value = counter.levelOne;
        levelOneRewardAmounts[0].text = "3";
        levelOneRewardAmounts[1].text = "1";
        if (counter.levelOne > 3 && !oneLevelOne)
        {
            string reward = levelOneRewardAmounts[0].text;
            string questName = "Meet the townspeople";
            oneLevelOne = true;
            levelOneRewardAmounts[0].color = Color.green;
            stats.coins += 3;
            StartCoroutine(AwardQuest(questName, reward));
        }
        if (stats.discoverBlacksmith && !twoLevelOne)
        {
            string reward = levelOneRewardAmounts[1].text;
            string questName = "Visit the blacksmith";
            levelOneSliders[1].value = 1;
            twoLevelOne = true;
            levelOneRewardAmounts[1].color = Color.green;
            stats.coins++;
            StartCoroutine(AwardQuest(questName, reward));
        }

        // Level two quests
        levelTwoRewardAmounts[0].text = "10";
        if (stats.defeatedWarrior & !oneLevelTwo)
        {
            string reward = levelTwoRewardAmounts[0].text;
            string questName = "Defeat the warrior";
            oneLevelTwo = true;
            levelTwoSliders[0].value = 1;
            levelTwoRewardAmounts[0].color = Color.green;
            stats.coins += 10;
            StartCoroutine(AwardQuest(questName, reward));
        }

        // Level three quests
        levelThreeRewardAmounts[0].text = "4";
        if (counter.npcUlric > 0 && !oneLevelThree)
        {
            string reward = levelThreeRewardAmounts[0].text;
            string questName = "Speak to the Elder";
            oneLevelThree = true;
            levelThreeSliders[0].value = 1;
            levelThreeRewardAmounts[0].color = Color.green;
            stats.coins += 4;
            StartCoroutine(AwardQuest(questName, reward));
        }

        levelThreeRewardAmounts[1].text = "3";
        if (stats.upgradesPurchased > 0 && !twoLevelThree)
        {
            string reward = levelThreeRewardAmounts[1].text;
            string questName = "Upgraded";
            twoLevelThree = true;
            levelThreeSliders[1].value = 1;
            levelThreeRewardAmounts[1].color = Color.green;
            stats.coins += 3;
            StartCoroutine(AwardQuest(questName, reward));
        }

        levelThreeRewardAmounts[2].text = "6";
        levelThreeSliders[2].value = stats.crabKills;
        if (stats.crabKills >= 3 && !threeLevelThree)
        {
            string reward = levelThreeRewardAmounts[2].text;
            string questName = "Crab killer";
            threeLevelThree = true;
            levelThreeRewardAmounts[2].color = Color.green;
            stats.coins += 6;
            StartCoroutine(AwardQuest(questName, reward));
        }

        levelThreeRewardAmounts[3].text = "3";
        levelThreeSliders[3].value = counter.levelThreeWarrior;
        if (counter.levelThreeWarrior >= 3 && !fourLevelThree)
        {
            string reward = levelThreeRewardAmounts[3].text;
            string questName = "Fighter";
            fourLevelThree = true;
            levelThreeRewardAmounts[3].color = Color.green;
            stats.coins += 3;
            StartCoroutine(AwardQuest(questName, reward));
        }

        levelFourRewardAmounts[0].text = "8";
        levelFourSliders[0].value = stats.pirateKills;
        if (stats.pirateKills == 4 && !oneLevelFour)
        {
            string reward = levelFourRewardAmounts[0].text;
            string questName = "Pirate sweeper";
            oneLevelFour = true;
            levelFourRewardAmounts[0].color = Color.green;
            stats.coins += 8;
            StartCoroutine(AwardQuest(questName, reward));
        }

        levelFourRewardAmounts[1].text = "25";
        if (stats.defeatedFinalBoss && !twoLevelFour)
        {
            levelFourSliders[1].value = 1;
            string reward = levelFourRewardAmounts[1].text;
            string questName = "Cove hero";
            twoLevelFour = true;
            levelFourRewardAmounts[1].color = Color.green;
            stats.coins += 25;
            StartCoroutine(AwardQuest(questName, reward));
        }
    }

    public IEnumerator AwardQuest(string quest, string reward)
    {
        questText.text = quest;
        rewardText.text = reward;
        anim.SetBool("open", true);
        SoundManager.instance.PurchaseSound();
        yield return new WaitForSeconds(3f);
        anim.SetBool("open", false);
        yield return null;
    }

    void Awake()
    {
        startButton.Select();
        questMenu.SetActive(false);
        Time.timeScale = 1f;
        quests = false;
        levelOneQuests.SetActive(true);
        levelTwoQuests.SetActive(false);
        levelThreeQuests.SetActive(false);
        levelFourQuests.SetActive(false);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        CheckForQuests();
        CheckForProgress();
    }

    void CheckForQuests()
    {
        if (SceneManager.GetActiveScene().name == "User-Interface" || !Input.GetKeyDown(KeyCode.Q)) return;
        
        if (quests)
        {
            Resume();
        }
        else if (!gamePaused && Time.timeScale != 0)
        {
            OpenQuestMenu();
        }
    }

    void OpenQuestMenu()
    {
        questMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        quests = true;
    }

    private void Resume()
    {
        if (gamePaused)
        {
            questMenu.SetActive(false);
            Time.timeScale = 1f;
            quests = false;
            gamePaused = false;
        }
    }
}
