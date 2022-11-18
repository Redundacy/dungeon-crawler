using System.Collections;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bossbehaviour : NetworkBehaviour 
{


    // Update is called once per frame
    void Update()
    {   
        // Boss is dead , game over
        if (Input.GetKeyDown("k"))
        {   
            Destroy(gameObject);
        }
    }

    
    

}
