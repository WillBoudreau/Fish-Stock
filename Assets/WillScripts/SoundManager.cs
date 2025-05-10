using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
     [Header("Audio clips")]
    [SerializeField] private AudioClip soundClip1;// The music clip for the main menu
    [SerializeField] private AudioClip soundClip2;// The music clip for the level
    [Header("References")]
    [SerializeField] private AudioSource soundSource;// The AudioSource component for music
    public AudioMixer audioMixer;// Reference to the AudioMixer for volume control
    /// <summary>
    /// Plays the specified music clip. Based on a string name.
    /// </summary>
    /// <param name="clipName">The name of the music clip to play.</param>
    /// <param name="loop">Whether to loop the music.</param>
    public void PlaySound(string clipName, bool loop = false)
    {
        switch(clipName)
        {
            
        }
        soundSource.loop = loop;
        soundSource.Play();
    }
    /// <summary>
    /// Changes the volume of the music.
    /// </summary>
    /// <param name="volume">Volume of the music.</param>
    /// <param name="groupName">The name of the AudioMixer group to set the volume for.</param>
    public void ChangeVolume(float volume, string groupName)
    {
        audioMixer.SetFloat(groupName,volume);
    }
}
