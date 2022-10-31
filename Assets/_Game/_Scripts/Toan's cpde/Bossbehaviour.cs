
using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bossbehaviour : NetworkBehaviour 
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {   
            Destroy(gameObject);
            Debug.Log("You win");
            // StartCoroutine(ExampleCoroutine());
            
        }
    }

    // IEnumerator ExampleCoroutine(){
    //     yield return new WaitForSeconds(5);
    //     MatchmakingService.LeaveLobby();
    //     SceneManager.LoadScene("Auth" , LoadSceneMode.Single);
    // }
}
