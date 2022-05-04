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

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(SoundManager.MUSIC_KEY, 0.5f);
        effectsSlider.value = PlayerPrefs.GetFloat(SoundManager.SFX_KEY, 0.5f);
    }

    private void SetMusicVolume(float valueMusic)
    {
        mixer.SetFloat(MUSIC, Mathf.Log10(valueMusic) * 20);
    }

    private void SetSfxVolume(float valueSFX)
    {
        mixer.SetFloat(SFX, Mathf.Log10(valueSFX) * 20);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(SoundManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(SoundManager.SFX_KEY, effectsSlider.value);
    }
}