using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private int playerCount;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Tree treePrefab;
    [SerializeField] private Transform spawnPos;


    private void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {
            //Create a new Player at a random position
            Player newPlayer = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity, transform);
            newPlayer.Initialize();

            //Create a tree for the player at a random position
            Tree newTree = Instantiate(treePrefab, spawnPos.position + Vector3.back*2, Quaternion.identity, transform);
            newTree.Initialize(newPlayer.PlayerId);
        }
    }
}
