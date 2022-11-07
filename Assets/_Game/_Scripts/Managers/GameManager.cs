using Unity.Netcode;
using UnityEngine;
using System;
using System.Collections;



public class GameManager : NetworkBehaviour {
    public PlayerController _playerPrefab;

    public int n = -1;

    [SerializeField] private PlayerController[] selection = default;
    
    public override void OnNetworkSpawn() {
        StartCoroutine(playerChoice());
    }   


    private IEnumerator playerChoice()
    {
        bool done = false;
        while(!done) // essentially a "while true", but with a bool to break out naturally
        {   
            if(n != -1)
            {                
                done = true; // breaks the loop
                SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,  n);
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
    
        // now this function returns
    }

        
    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong playerId , int n) {
        var spawn = Instantiate(selection[n]);
        spawn.NetworkObject.SpawnWithOwnership(playerId);
    }


    public override void OnDestroy() {
        base.OnDestroy();
        MatchmakingService.LeaveLobby();
        if(NetworkManager.Singleton != null )NetworkManager.Singleton.Shutdown();
    }

}