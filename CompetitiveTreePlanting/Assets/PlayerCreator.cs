using Fusion;
using Fusion.Sockets;
using MultiplayerDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Fusion.NetworkCharacterController;

public class PlayerCreator : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private bool DrawSpawnArea = false;
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private NetworkPrefabRef _treePrefab;
    [SerializeField] private Vector3 spawnAreaSize;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private MusicManager musicManager;

    private Player playerReference;
    private Tree treeReference;
    private Vector3 spawnAreaCenter;
    private int maxLevel = 0;

    private Dictionary<PlayerRef, NetworkObject[]> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject[]>();

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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.localPosition + spawnAreaCenter, spawnAreaSize);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            int spawnIndex = (player.RawEncoded % runner.Config.Simulation.DefaultPlayers);

            //Vector3 spawnPosition = CreateSpawnPosition();
            Vector3 spawnPosition = new Vector3(spawnIndex * 3, 2, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            playerReference = networkPlayerObject.GetComponent<Player>();

            //Initialize the Player to set his personal id --> Must be converted to Fusion Callback on player
            playerReference.Initialize();
            spawnAreaCenter = playerReference.transform.position;

            //Create a tree for the player at a random position --> Must be converted to Fusion Callback on player
            NetworkObject networkTreeObject = runner.Spawn(_treePrefab, spawnPositions[spawnIndex].position, Quaternion.identity, player);
            treeReference = networkTreeObject.GetComponent<Tree>();
            treeReference.GetComponent<LevelUp>().TreeGrownUp.AddListener(TreeLeveledUp);

            treeReference.Initialize(playerReference.PlayerId);
            playerReference.SetTree(treeReference);

            _spawnedCharacters.Add(player, new NetworkObject[] { networkPlayerObject, networkTreeObject });
        }
    }

    
    private void TreeLeveledUp(int level)
    {
        if(maxLevel < level)
        {
            maxLevel = level;
            musicManager.RPC_IncreaseTension(maxLevel);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject[] networkObjects))
        {
            runner.Despawn(networkObjects[0]);
            runner.Despawn(networkObjects[1]);
            _spawnedCharacters.Remove(player);
        }
    }

    private GGJInputActions inputControls;

    private void Awake()
    {
        inputControls = new GGJInputActions();
        inputControls.Player.Interact.performed += _ => Interact();
        inputControls.Player.Hit.performed += _ => TryHit();
        inputControls.Player.Dash.performed += _ => TryDash();
        inputControls.Player.Movement.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        inputControls.Player.Movement.canceled += ctx => moveVector = Vector2.zero;
    }

    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    private void TryDash()
    {
        _dashButton = true;
    }

    private void TryHit()
    {
        _hitButton = true;
    }

    private void Interact()
    {
        _interactButton = true;
    }

    private bool _interactButton;
    private bool _hitButton;
    private bool _dashButton;
    private Vector2 moveVector;
    private float fromToAngle = 0f;

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();
        data.direction = moveVector;
        data.toRotation = moveVector.magnitude < 0.01f ? Quaternion.identity : Quaternion.LookRotation(new Vector3(moveVector.x, 0, moveVector.y), Vector3.up); ;

        if (_interactButton)
            data.buttons |= NetworkInputData.INTERACT;
        _interactButton = false;

        if (_hitButton)
            data.buttons |= NetworkInputData.HIT;
        _hitButton = false;

        if (_dashButton)
            data.buttons |= NetworkInputData.DASH;
        _dashButton = false;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, System.ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    private NetworkRunner _runner;

    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }
    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }


}
