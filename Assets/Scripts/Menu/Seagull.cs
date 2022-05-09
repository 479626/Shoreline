using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Seagull : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb;
    [SerializeField] AudioSource audio;
    [SerializeField] List<AudioClip> seagullClips = new List<AudioClip>();
    [SerializeField] AudioMixer mixer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity.x = 6;
        StartCoroutine(Timer());
    }

    private void Update()
    {
        MoveSeagull();
    }

    private void MoveSeagull()
    {
        rb.velocity = velocity;
    }

    private void PlaySound()
    {
        AudioClip clip = seagullClips[Random.Range(0, seagullClips.Count)];

        audio.PlayOneShot(clip);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(Random.Range(0, 25));
        PlaySound();
        yield return null;
    }
}
