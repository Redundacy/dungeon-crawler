using System;
using Unity.Netcode;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour {
    [SerializeField] private float _speed = 3;

    [SerializeField] GameObject pauseMenu; 

    public GameObject Boss;

    private Rigidbody _rb;

    // Used to keep track when the player pasue the game
    private float oldSpeed;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        oldSpeed = _speed;
        Boss = GameObject.FindGameObjectWithTag("Boss");
    }


    private void Update() {
        var dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _rb.velocity = dir * _speed;

        // If the player pressed , the pauseMenu should appear, as well as the player movement should halt
        // Else , resume as usual
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenu.activeInHierarchy){
                pauseMenu.SetActive(false);
                _speed = oldSpeed;
            } 
            else PauseMenuExit();
            
        }

        // Boss is dead , game over
        if (Boss == null){
            StartCoroutine(ExampleCoroutine());
        }
    }

    public override void OnNetworkSpawn() {
        if (!IsOwner) Destroy(this);
    }


    IEnumerator ExampleCoroutine(){
        yield return new WaitForSeconds(3);
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Auth" , LoadSceneMode.Single);
    }

    public void PauseMenuExit(){
        pauseMenu.SetActive(true);
        _speed = 0;
    }
}