using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomId;
    
    // This action help exiting out of the game and lobby to not throw errors
    public static event Action LobbyLeft;
    
    public void Return(int roomId){
        SceneManager.LoadSceneAsync(roomId);
        LobbyLeft?.Invoke();
    }

}
