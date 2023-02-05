using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : NetworkBehaviour
{
    [SerializeField, Header("Music should be ordered from normal to dramatic.")] 
    private AudioSource[] musicSources;

    [SerializeField] 
    private AudioSource currentSong;
    
    private int currentIndex = 0;

    //[Rpc(RpcSources.All, RpcTargets.All)]
    public void IncreaseTension(int lvl)
    {
        if (lvl > currentIndex)
        {
            currentIndex++;
            currentIndex = (musicSources.Length + currentIndex) % musicSources.Length;
            TransitionTo(musicSources[currentIndex]);
        }
    }

    /*public void DecreaseTension()
    {
        currentIndex = (musicSources.Length + currentIndex - 1) % musicSources.Length;
        TransitionTo(musicSources[currentIndex]);
    }*/

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
