using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip clip1;
    public AudioClip clip2;

    public AudioClip clip3;

    public AudioClip backgroundMusic;
    public AudioSource[] audiosources;

    System.Random r;

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

        r = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSplash()
    {

        int rInt = r.Next(0, 2); //for ints
        switch (rInt)
        {
            case 0:
                audiosources[1].clip = clip1;
                audiosources[1].Play(); break;

            case 1:
                audiosources[1].clip = clip2;
                audiosources[1].Play(); break;

            case 2:
                audiosources[1].clip = clip3;
                audiosources[1].Play(); break;
        }



    }
}
