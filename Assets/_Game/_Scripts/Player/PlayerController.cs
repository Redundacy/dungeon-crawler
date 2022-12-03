using System;
using Unity.Netcode;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// Script handle client player misc ( camera , pauseMenu)
public class PlayerController : NetworkBehaviour {
    [SerializeField] GameObject playerCam; 

    [SerializeField] GameObject freeLook; 
    [SerializeField] GameObject pauseMenu; 

    [SerializeField] GameObject winMenu; 

    public GameObject Boss;

    private Rigidbody _rb;

    private void Awake() {
        Boss = GameObject.FindGameObjectWithTag("Boss");
    }


    private void Update() {
        // If the player pressed , the pauseMenu should appear, as well as the player movement should halt
        // Else , resume as usual
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenu.activeInHierarchy){
                pauseMenu.SetActive(false);
            } 
            else PauseMenuExit();
            
        }

        if (Boss == null)
        {   
            StartCoroutine(ExampleCoroutine());
        }
    }

    public override void OnNetworkSpawn() {
        if (!IsOwner)
        {
            Destroy(playerCam);
            Destroy(this);
        }
        if (IsOwner){
            playerCam.SetActive(true);
            freeLook.SetActive(true);
        } 
    }


    public void PauseMenuExit(){
        pauseMenu.SetActive(true);
    }

    IEnumerator ExampleCoroutine(){
        winMenu.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Auth" , LoadSceneMode.Single);
    }
}