using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField, Header("Music should be ordered from normal to dramatic.")] 
    private AudioSource[] musicSources;

    [SerializeField] 
    private AudioSource currentSong;
    
    private int currentIndex = 0;

    public void IncreaseTension()
    {
        currentIndex = (musicSources.Length + currentIndex + 1) % musicSources.Length;
        TransitionTo(musicSources[currentIndex]);
    }

    public void DecreaseTension()
    {
        currentIndex = (musicSources.Length + currentIndex - 1) % musicSources.Length;
        TransitionTo(musicSources[currentIndex]);
    }

    private void TransitionTo(AudioSource target)
    {
        if(currentSong == null)
        {
            currentSong = target;
            currentSong.mute = false;
            return;
        }

        currentSong.mute = true;
        currentSong = target;
        currentSong.mute = false;
    }
}