using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterSelection : NetworkBehaviour
{
    public GameManager gameManager;

    [SerializeField] GameObject chooseMenu; 

    [SerializeField] private int value;
    
    public void Choosing(){
        gameManager.n = value;
        chooseMenu.SetActive(false);        
    }
}
