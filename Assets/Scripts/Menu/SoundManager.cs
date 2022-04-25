using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource swingSource, doorSource, clickSource, walkSource, attackSource, maleSpeechSource, femaleSpeechSource, purchaseSource, failSource;
    [SerializeField] List<AudioClip> swingClips, doorClips, clickClips, walkClips, attackClips, maleSpeechClips, femaleSpeechClips, purchaseClips, failClips = new List<AudioClip>();
    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";
    public const string MASTER_KEY = "MasterVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.5f);

        mixer.SetFloat(Sliders.MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(Sliders.SFX, Mathf.Log10(sfxVolume) * 20);
    }

    public void SwingSound()
    {
        AudioClip clip = swingClips[Random.Range(0, swingClips.Count)];

        swingSource.PlayOneShot(clip);
    }

    public void WalkSound()
    {
        AudioClip clip = walkClips[Random.Range(0, walkClips.Count)];

        walkSource.PlayOneShot(clip);
    }

    public void PurchaseSound()
    {
        AudioClip clip = purchaseClips[Random.Range(0, purchaseClips.Count)];

        purchaseSource.PlayOneShot(clip);
    }

    public void AttackSound()
    {
        AudioClip clip = attackClips[Random.Range(0, attackClips.Count)];

        attackSource.PlayOneShot(clip);
    }

    public void FailSound()
    {
        AudioClip clip = failClips[Random.Range(0, failClips.Count)];

        failSource.PlayOneShot(clip);
    }

    public void MaleSpeechSound()
    {
        AudioClip clip = maleSpeechClips[Random.Range(0, maleSpeechClips.Count)];

        maleSpeechSource.PlayOneShot(clip);
    }

    public void FemaleSpeechSound()
    {
        AudioClip clip = femaleSpeechClips[Random.Range(0, femaleSpeechClips.Count)];

        femaleSpeechSource.PlayOneShot(clip);
    }

    public void DoorSound()
    {
        AudioClip clip = doorClips[Random.Range(0, doorClips.Count)];

        doorSource.PlayOneShot(clip);
    }

    public void ClickSound()
    {
        AudioClip clip = clickClips[Random.Range(0, clickClips.Count)];

        clickSource.PlayOneShot(clip);
    }
}