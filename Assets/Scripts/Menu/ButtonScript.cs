using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonScript : MonoBehaviour
{
    public GameObject startButton;
    public Animator animator;
    public InteractionCounter count;
    [SerializeField] AudioMixer mixer;

    public float transitionTime = 1f;

    void Start()
    {
        startButton.GetComponent<Button>().Select();
    }

    #region Button Methods

    public void OnStartButton()
    {
        count.levelOne = 0;
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

    #endregion
}
