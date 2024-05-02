using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource musicSource;
    public AudioClip[] musics;

    private Boss boss;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();

        instance = this;

        musicSource.clip = musics[0];
        musicSource.Play();
    }
}
