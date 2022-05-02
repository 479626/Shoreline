using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sliders : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;

    public const string MUSIC = "MusicVolume";
    public const string SFX = "SFXVolume";
    public const string MASTER = "MasterVolume";

    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(SoundManager.MUSIC_KEY, 0.5f);
        effectsSlider.value = PlayerPrefs.GetFloat(SoundManager.SFX_KEY, 0.5f);
    }

    void SetMusicVolume(float valueMusic)
    {
        mixer.SetFloat(MUSIC, Mathf.Log10(valueMusic) * 20);
    }

    void SetSFXVolume(float valueSFX)
    {
        mixer.SetFloat(SFX, Mathf.Log10(valueSFX) * 20);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(SoundManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(SoundManager.SFX_KEY, effectsSlider.value);
    }
}