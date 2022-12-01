using System;
using Unity.Netcode;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// Script handle client player misc ( camera , pauseMenu)
public class PlayerController : NetworkBehaviour {
    [SerializeField] GameObject playerCam; 
    [SerializeField] GameObject pauseMenu; 

    public GameObject Boss;

    private Rigidbody _rb;

    // Used to keep track when the player pasue the game
    //private float oldSpeed;

    private void Awake() {
        Boss = GameObject.FindGameObjectWithTag("Boss");
    }


    private void Update() {
        // If the player pressed , the pauseMenu should appear, as well as the player movement should halt
        // Else , resume as usual
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenu.activeInHierarchy){
                pauseMenu.SetActive(false);
                //_speed = oldSpeed;
            } 
            else PauseMenuExit();
            
        }
    }

    public override void OnNetworkSpawn() {
        if (!IsOwner) Destroy(this);
        if (IsOwner) playerCam.SetActive(true);
    }


    public void PauseMenuExit(){
        pauseMenu.SetActive(true);
        //_speed = 0;
    }
}