using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip clip1;
    public AudioClip clip2;

    public AudioClip backgroundMusic;
    public AudioSource[] audiosources;

    //public AudioSource audioSourceBackground;

    // Start is called before the first frame update
    void Start()
    {
        audiosources = GetComponents<AudioSource>();
        Debug.Log(audiosources);
        audiosources[0].clip = backgroundMusic;
        audiosources[0].Play();

        // audioSourceBackground = GetComponent<AudioSource>();
        //audioSourceBackground.clip = backgroundMusic;
        //audioSourceBackground.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSplash()
    {
        System.Random r = new System.Random();
        int rInt = r.Next(0, 1); //for ints
        if (rInt == 0)
        {
            audiosources[1].clip = clip1;
            audiosources[1].Play();
        }
        else
        {
            audiosources[1].clip = clip2;
            audiosources[1].Play();
        }

    }
}
