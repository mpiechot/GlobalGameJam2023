using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using static Fusion.NetworkCharacterController;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    PlayerCreator mp_creator;

    private void OnEnable()
    {
        mp_creator.OnConnected += Connected;
    }

    private void OnDisable()
    {
        mp_creator.OnConnected -= Connected;
    }

    private void Connected()
    {
        gameObject.SetActive(false);
    }

    public void Host()
    {
        mp_creator.StartGame(GameMode.Host);
        gameObject.SetActive(false);

    }

    public void Join()
    {
        mp_creator.StartGame(GameMode.Client);

    }
}
