using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour {
    [SerializeField] private PlayerController _playerPrefab;
    private readonly Dictionary<ulong, PlayerTransform> _playersInGame = new();

    public enum GameState
    {
        Allocation,
        Combat
    }
    public GameState CurrentGameState = GameState.Allocation;

    public override void OnNetworkSpawn() {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    public void Update()
    {
        if (_playersInGame.All(p => p.Value.GetNetworkReady()))
        {
            //ReadyClientRpc();
            foreach (var player in _playersInGame)
            {
                player.Value.UpdateState();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong playerId) {
        var spawn = Instantiate(_playerPrefab);
        spawn.NetworkObject.SpawnWithOwnership(playerId);
        _playersInGame.Add(playerId, spawn.transform.GetComponentInChildren<PlayerTransform>());

    }

    public override void OnDestroy() {
        base.OnDestroy();
        MatchmakingService.LeaveLobby();
        if(NetworkManager.Singleton != null )NetworkManager.Singleton.Shutdown();
    }

    [ClientRpc]
    public void ReadyClientRpc()
    {
        
    }

    [ClientRpc]
    private void ChangeStateToCombatClientRpc(ulong clientId) {
        if (IsServer || IsOwner) return;
    }
}
