using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    private static AudioClip[] walkSounds;
    public AudioClip[] ManagerWalkSounds;
    private AudioSource[] audioSrc;
    private static AudioSource srcPlayer1;
    private static AudioSource srcPlayer2;
    private int chosen;
    private static float startVolume1;
    private static float startVolume2;
    
    void Start()
    {   
        walkSounds = ManagerWalkSounds;
        audioSrc = GetComponents<AudioSource>();
        srcPlayer1 = audioSrc[0];
        srcPlayer2 = audioSrc[1];     
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
                    int chosen = Random.Range(0, (int) walkSounds.Length);
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
            srcPlayer.PlayOneShot(walkSounds[val]);
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
