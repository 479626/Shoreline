using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Seagull : MonoBehaviour
{
    Vector2 velocity;
    private Rigidbody2D rb;
    [SerializeField] AudioSource audio;
    [SerializeField] List<AudioClip> seagullClips = new List<AudioClip>();
    [SerializeField] AudioMixer mixer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity.x = 6;
        StartCoroutine(Timer());
    }

    void Update()
    {
        rb.velocity = velocity;
    }

    void PlaySound()
    {
        AudioClip clip = seagullClips[Random.Range(0, seagullClips.Count)];

        audio.PlayOneShot(clip);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(Random.Range(0, 25));
        PlaySound();
        yield break;
    }
}
