using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    private AudioSource audioSrc;
    public AudioClip walkSound;
    public AudioClip trailSound;
    private const string WALK_CLIP_NAME = "walk";
    private const string TRAIL_CLIP_NAME = "trail";
    public float volumeWalk = 100;
    public float volumeTrail = 100;
    public float fadeOutFactor = 0.01f;
    public float fadeInFactor = 0.01f;


    void Start()
    {
        audioSrc = GetComponents<AudioSource>()[0];
        if (tag == "Player1"){
            audioSrc.panStereo = -1; // left ear
        }else{
             audioSrc.panStereo = 1; // right ear
        }
        
    }
    public void PlaySound(string clip)
    {
        float maxVolume = 100;
        AudioClip playSound = null;
        switch (clip)
        {
            case WALK_CLIP_NAME:
                maxVolume = volumeWalk;
                playSound = walkSound;
                break;

            case TRAIL_CLIP_NAME:
                maxVolume = volumeTrail;
                playSound = trailSound;
                break;
        }    
        if (audioSrc.volume < maxVolume)
        {
            audioSrc.volume += 0.01f;
        }
        if (!audioSrc.isPlaying)
        {    
            audioSrc.clip = playSound;
            audioSrc.volume = fadeInFactor;
            audioSrc.Play();
            
        }
    }

    public void StopPlaying()
    {
        float startVolume = audioSrc.volume;
        //Fade sound 
        if (audioSrc.volume > 0)
        {
            audioSrc.volume -= fadeOutFactor;
        }else{
           audioSrc.Stop(); 
        }

    }

}
