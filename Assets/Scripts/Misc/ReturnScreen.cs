using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ReturnScreen : NetworkBehaviour
{
    // Start is called before the first frame update
    public int roomId;

    
    public async void Return(int roomId){
        NetworkManager.Singleton.Shutdown();
        await MatchmakingService.LeaveLobby();
        SceneManager.LoadSceneAsync(roomId);
    }

}
