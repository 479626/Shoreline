using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Animator anim;
    public static LevelManager instance;
    public Slider slider;
    public Text progress;
    public Text tip;

    [SerializeField] private List<string> gameTips, jokes, controlInfo = new List<string>();

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            int decision = Random.Range(0, 2);

            if (decision == 0)
            {
                string message = jokes[Random.Range(0, jokes.Count)];

                tip.text = message;
            }
            if (decision == 1)
            {
                string message = gameTips[Random.Range(0, gameTips.Count)];

                tip.text = message;
            }
            if (decision == 2)
            {
                string message = controlInfo[Random.Range(0, controlInfo.Count)];

                tip.text = message;
            }
            StartCoroutine(Initialize());
        }
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
        yield break;
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(2f);
        LoadLevel(1);
        yield break;
    }
}
