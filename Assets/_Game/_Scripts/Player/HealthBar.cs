using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class HealthBar : NetworkBehaviour
{   
    private const float MAX_HEALTH = 100f;

    private Image healthBar;

    public float health = MAX_HEALTH;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    private void ModifyHP(int amount){
        health += amount;
        healthBar.fillAmount = health / MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
         // don't do anything if you're not the owner
        if (!IsOwner) return;
        
        if(Input.GetKeyDown(KeyCode.Space)){
            RequestFireServerRpc();
            ModifyHP(-10);
        }
        
    }

    [ServerRpc]
    private void RequestFireServerRpc() {
        FireClientRpc();
    }

    [ClientRpc]
    private void FireClientRpc() {
        if (!IsOwner) ModifyHP(-10);
    }



}
