
using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;


public class Bossbehaviour : NetworkBehaviour 
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {   
            Destroy(gameObject);
            
        }
    }

}
