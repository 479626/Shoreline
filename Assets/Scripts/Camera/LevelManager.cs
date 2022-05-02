using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public PlayerStats stats;
    public Animator anim;
    public static LevelManager instance;
    public Slider slider;
    public Text progress;
    public Text tip;
    private string message;

    [SerializeField] private List<string> gameTips, jokes, controlInfo = new List<string>();
    
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            int decision = Random.Range(0, 2);

            switch (decision)
            {
                case 0:
                    message = jokes[Random.Range(0, jokes.Count)];
                    break;
                case 1:
                    message = gameTips[Random.Range(0, gameTips.Count)];
                    break;
                case 2:
                    message = controlInfo[Random.Range(0, controlInfo.Count)];
                    break;
            }
            tip.text = message;

            StartCoroutine(Initialize());
        }
    }

    public void LoadLevel(int buildIndex)
    {
        StartCoroutine(LoadAsync(buildIndex));
    }

    public IEnumerator LoadAsync(int buildIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            slider.value = operation.progress;
            progress.text = operation.progress * 100 + "%";

            yield return null;
        }

        if (operation.isDone)
        {
            anim.SetTrigger("Load");
        }
        yield return null;
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(2f);
        LoadLevel(1);
        yield return null;
    }
}
