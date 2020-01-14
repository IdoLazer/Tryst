using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static AudioClip BooSound, YaySound, maskPop, jump, splash, Staring;
    static AudioSource src;
    void Start()
    {
        //BooSound = Resources.Load<AudioClip>("Boo1");
        // name  = Resources.Load<AudioClip>("clip_name");

        src = GetComponent<AudioSource>();
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            //case "boo":
              //  src.PlayOneShot(BooSound);
                //break;
            //case "cheer":
              //  src.PlayOneShot(YaySound);
                //break;
        }
    }
}