
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip clip1, clip2, gong, explosion, special, backgroundMusic, clip3;
    public AudioSource[] audiosources;

    System.Random r;

    // Start is called before the first frame update
    void Start()
    {
        audiosources = GetComponents<AudioSource>();
        audiosources[0].clip = backgroundMusic;
        audiosources[0].Play();

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

    public void playStartSound()
    {
        audiosources[1].clip = gong;
        audiosources[1].Play();
    }

    public void playExplosion()
    {
        audiosources[1].clip = explosion;
        audiosources[1].Play();
    }

    public void playSpecialSound()
    {
        audiosources[1].clip = special;
        audiosources[1].Play();
    }
}
