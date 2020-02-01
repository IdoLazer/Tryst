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

    [Range(0, 1)]
    public float volumeWalk = 1;
    [Range(0, 1)]
    public float volumeTrail = 1;
    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 1f;
    public float fadeOutBetweenSpeed = 2f;
   [Range(-3, 3)]
    public float normalPitch = 1f;
    [Range(-3, 3)]
    public float pitchWalkLeft = 0.5f;
    [Range(-3, 3)]
    public float pitchWalkRight = 1.5f;
    [Range(-3, 3)]
    public float pitchTrailLeft = 0.5f;
    [Range(-3, 3)]
    public float pitchTrailRight = 1.5f;
    public float pitchFadeSpeed = 1f;


    void Start()
    {
        audioSrc = GetComponents<AudioSource>()[0];
        if (tag == "Player1")
        {
            audioSrc.panStereo = -1; // left ear
        }
        else
        {
            audioSrc.panStereo = 1; // right ear
        }

    }
    public void PitchSound(string clip, float direction)
    {
        float pitch = normalPitch;
        if (direction != 0)
        {
            switch (clip)
            {
                case WALK_CLIP_NAME:
                    pitch = direction < 0 ? pitchWalkLeft : pitchWalkRight;
                    break;

                case TRAIL_CLIP_NAME:
                    pitch = direction < 0 ? pitchTrailLeft : pitchTrailRight;
                    break;
            }
        }
        if (audioSrc.pitch < pitch)
        {
            audioSrc.pitch += pitchFadeSpeed / 100;
            audioSrc.pitch = audioSrc.pitch > pitch ? pitch : audioSrc.pitch; //normalize pitch
        }
        else
        {
            audioSrc.pitch -= pitchFadeSpeed / 100;
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

        //Fade out old sound
        if (audioSrc.clip != playSound)
        {
            StopPlaying(true);
        }
        //Fad in sound   
        else if (audioSrc.volume < maxVolume)
        {
            audioSrc.volume += fadeInSpeed / 100;
        }

        //Replace sound
        if (!audioSrc.isPlaying)
        {
            audioSrc.clip = playSound;
            audioSrc.Play();
        }
    }

    public void StopPlaying(bool fadeBetween = false)
    {
        float factor = fadeBetween ? fadeOutBetweenSpeed : fadeOutSpeed;
        float startVolume = audioSrc.volume;
        //Fade out sound 
        if (audioSrc.volume > 0)
        {
            audioSrc.volume -= factor / 100;
        }
        else
        {
            audioSrc.Stop();
        }

    }

}
