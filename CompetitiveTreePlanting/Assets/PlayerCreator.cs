using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Tree treePrefab;

    private Vector3 spawnAreaCenter;
    [SerializeField] private Vector3 spawnAreaSize;
    [SerializeField] private bool DrawSpawnArea = false;

    private void Start()
    {
        //Initialize the Player to set his personal id
        player.Initialize();
        spawnAreaCenter = player.transform.position;

        //Create a tree for the player at a random position
        Tree newTree = Instantiate(treePrefab, CreateSpawnPosition(), Quaternion.identity, transform);
        newTree.Initialize(player.PlayerId);
        player.SetTree(newTree);
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
        if (!DrawSpawnArea)
        {
            return;
        }
        spawnAreaCenter = player.transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.localPosition + spawnAreaCenter, spawnAreaSize);
    }
}
