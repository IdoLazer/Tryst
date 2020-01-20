using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    private static AudioClip walk1, walk2, walk3, walk4, walk5, walk6;
    static AudioSource src;
    private int chosen;
    void Start()
    {
        walk1 = Resources.Load<AudioClip>("player-a-walks-01");
        walk2 = Resources.Load<AudioClip>("player-a-walks-02");
        walk3 = Resources.Load<AudioClip>("player-a-walks-03"); 
        walk4 = Resources.Load<AudioClip>("player-a-walks-04");
        walk5 = Resources.Load<AudioClip>("player-a-walks-05");
        walk6 = Resources.Load<AudioClip>("player-a-walks-06");


        src = GetComponent<AudioSource>();
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {

            case "walk":
                if (!src.isPlaying)
                {
                    int chosen = Random.Range(0, 7);
                    SoundManger.playWalk(chosen);
                }

                break;
                //case "cheer":
                //  src.PlayOneShot(YaySound);
                //break;
        }
    }
    private static void playWalk(int val)
    {
        if(val==1)
        {
            src.PlayOneShot(walk1);
        }
        if (val == 2)
        {
            src.PlayOneShot(walk2);
        }
        if (val == 3)
        {
            src.PlayOneShot(walk3);
        }
        if (val == 4)
        {
            src.PlayOneShot(walk4);
        }
        if (val == 5)
        {
            src.PlayOneShot(walk5);
        }
        if (val == 6)
        {
            src.PlayOneShot(walk6);
        }
    }

    public static void StopPlaying()
    {
        src.Stop();
    }

}
