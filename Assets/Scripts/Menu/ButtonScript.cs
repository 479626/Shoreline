using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour
{
    public GameObject startButton;
    public GameObject optionButton;
    public GameObject quitButton;
    public GameObject muteButtonOn;
    public GameObject muteButtonOff;
    [SerializeField] AudioMixer mixer;

    public Animator transition;
    public float transitionTime = 1f;

    void Start()
    {
        startButton.GetComponent<Button>().Select();
    }

    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnStartButton()
    {
        StartCoroutine(LoadLevel(1));
    }


    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(LevelIndex);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnMuteButtonOn()
    {
        float musicVolume = PlayerPrefs.GetFloat(SoundManager.MUSIC_KEY, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SoundManager.SFX_KEY, 0.5f);

        mixer.SetFloat(Sliders.MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(Sliders.SFX, Mathf.Log10(sfxVolume) * 20);
    }

    public void OnMuteButtonOff()
    {
        mixer.SetFloat(Sliders.MUSIC, -80f);
        mixer.SetFloat(Sliders.SFX, -80f);
    }

    public void OnButtonClick()
    {
        SoundManager.instance.ClickSound();
    }
}
