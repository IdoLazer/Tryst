using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    private static AudioClip walk1, walk2, walk3, walk4, walk5, walk6;
    private static AudioSource[] src;
    private static AudioSource srcPlayer1;
    private static AudioSource srcPlayer2;
    private int chosen;
    private static float startVolume1;
    private static float startVolume2;
    
    void Start()
    {    
        walk1 = Resources.Load<AudioClip>("player-a-walks-01");
        walk2 = Resources.Load<AudioClip>("player-a-walks-02");
        walk3 = Resources.Load<AudioClip>("player-a-walks-03"); 
        walk4 = Resources.Load<AudioClip>("player-a-walks-04");
        walk5 = Resources.Load<AudioClip>("player-a-walks-05");
        walk6 = Resources.Load<AudioClip>("player-a-walks-06");

        src = GetComponents<AudioSource>();
        srcPlayer1 = src[0];
        srcPlayer2 = src[1];     
        startVolume1 = srcPlayer1.volume;
        startVolume2 = srcPlayer2.volume;
    }
    public static void PlaySound_Player1(string clip){
        PlaySound(clip, srcPlayer1, startVolume1);
    }
    public static void PlaySound_Player2(string clip){
        PlaySound(clip, srcPlayer2 , startVolume2);
    }
    private static void PlaySound(string clip, AudioSource srcPlayer , float startVolume)
    {
        srcPlayer.volume = startVolume;
        switch (clip)
        {

            case "walk":
                if (!srcPlayer.isPlaying)
                {
                    int chosen = Random.Range(0, 7);
                    SoundManger.playWalk(chosen , srcPlayer);
                }

                break;
                //case "cheer":
                //  src.PlayOneShot(YaySound);
                //break;
        }
    }
    private static void playWalk(int val, AudioSource srcPlayer)
    {
        if(val==1)
        {
            srcPlayer.PlayOneShot(walk1);
        }
        if (val == 2)
        {
            srcPlayer.PlayOneShot(walk2);
        }
        if (val == 3)
        {
            srcPlayer.PlayOneShot(walk3);
        }
        if (val == 4)
        {
            srcPlayer.PlayOneShot(walk4);
        }
        if (val == 5)
        {
            srcPlayer.PlayOneShot(walk5);
        }
        if (val == 6)
        {
            srcPlayer.PlayOneShot(walk6);
        }
    }

    private static void StopPlaying(AudioSource srcPlayer , float startVolume)
    {
        if (srcPlayer.volume > 0) {
            srcPlayer.volume -= startVolume * Time.deltaTime;
        }

    }
    public static void StopPlaying_Player1()
    {
        StopPlaying(srcPlayer1 , startVolume1); 
    }
    public static void StopPlaying_Player2()
    {
        StopPlaying(srcPlayer2 , startVolume2); 
    }

}
