using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource swordSource;
    [SerializeField] List<AudioClip> swordClips = new List<AudioClip>();
    [SerializeField] AudioSource walkSource;
    [SerializeField] List<AudioClip> walkClips = new List<AudioClip>();
    [SerializeField] AudioSource jumpSource;
    [SerializeField] List<AudioClip> jumpClips = new List<AudioClip>();
    [SerializeField] AudioSource blockSource;
    [SerializeField] List<AudioClip> blockClips = new List<AudioClip>();
    [SerializeField] AudioSource clickSource;
    [SerializeField] List<AudioClip> clickClips = new List<AudioClip>();

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

    public void SwordSound()
    {
        AudioClip clip = swordClips[Random.Range(0, swordClips.Count)];

        swordSource.PlayOneShot(clip);
    }

    public void WalkSound()
    {
        AudioClip clip = walkClips[Random.Range(0, walkClips.Count)];

        walkSource.PlayOneShot(clip);
    }

    public void JumpSound()
    {
        AudioClip clip = jumpClips[Random.Range(0, jumpClips.Count)];

        jumpSource.PlayOneShot(clip);
    }

    public void BlockSound()
    {
        AudioClip clip = blockClips[Random.Range(0, blockClips.Count)];

        blockSource.PlayOneShot(clip);
    }

    public void ClickSound()
    {
        AudioClip clip = clickClips[Random.Range(0, clickClips.Count)];

        blockSource.PlayOneShot(clip);
    }
}