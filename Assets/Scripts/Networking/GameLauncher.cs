using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public static GameLauncher Instance { get; private set; }
    [SerializeField] private NetworkPlayer playerPrefab;

    private NetworkRunner _runner;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        if (_runner == null)
        {
            _runner = gameObject.AddComponent<NetworkRunner>();
        }
    }

    public async void Launch(GameMode mode)
    {
        DontDestroyOnLoad(gameObject);

        Debug.Log("Connecting to the game...");
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            PlayerCount = 2
        });
        Debug.Log("Connected!");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"{nameof(OnPlayerJoined)}");
        if (runner.IsServer)
        {
            NetworkPlayer networkPlayer = runner.Spawn(
                playerPrefab, Vector3.zero, Quaternion.identity, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"{nameof(OnPlayerLeft)}");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Debug.Log($"{nameof(OnInput)}");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log($"{nameof(OnInputMissing)}");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log($"{nameof(OnShutdown)}");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log($"{nameof(OnConnectedToServer)}");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log($"{nameof(OnDisconnectedFromServer)}");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log($"{nameof(OnConnectRequest)}");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log($"{nameof(OnConnectFailed)}");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log($"{nameof(OnUserSimulationMessage)}");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log($"{nameof(OnSessionListUpdated)}");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log($"{nameof(OnCustomAuthenticationResponse)}");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log($"{nameof(OnHostMigration)}");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log($"{nameof(OnReliableDataReceived)}");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}
