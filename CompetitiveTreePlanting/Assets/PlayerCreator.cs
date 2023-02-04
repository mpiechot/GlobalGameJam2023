using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private int playerCount;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Tree treePrefab;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private Vector3 spawnAreaCenter;
    [SerializeField] private Vector3 spawnAreaSize;
    [SerializeField] private bool DrawSpawnArea = false;


    private void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {
            //Create a new Player at a random position
            Player newPlayer = Instantiate(playerPrefab, CreateSpawnPosition(), Quaternion.identity, transform);
            newPlayer.Initialize();

            //Create a tree for the player at a random position
            Tree newTree = Instantiate(treePrefab, CreateSpawnPosition(), Quaternion.identity, transform);
            newTree.Initialize(newPlayer.PlayerId);
        }
    }

    private Vector3 CreateSpawnPosition()
    {
        return spawnAreaCenter + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));
    }

    private void OnDrawGizmos()
    {
        if(!DrawSpawnArea)
        {
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.localPosition + spawnAreaCenter, spawnAreaSize);
    }
}
