using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class HealthBar : NetworkBehaviour
{   
    private const float MAX_HEALTH = 10f;

    private Image healthBar;

    private NetworkVariable<int> _netHealth = new(writePerm: NetworkVariableWritePermission.Owner);

    private float tempSpeed = 0;

	public int health = (int)MAX_HEALTH;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        if(IsOwner)
        {
            _netHealth.Value = (int)MAX_HEALTH;
			tempSpeed = gameObject.GetComponentInParent<RigidBodyMovementScript>().Speed;
		}
    }

    public void ModifyHP(int amount){
        health += amount;
        healthBar.fillAmount = health / MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
		if (IsOwner)
		{
            if (health > MAX_HEALTH)
            {
                health = (int)MAX_HEALTH;
            }
            _netHealth.Value = health;
            if(_netHealth.Value <= 0)
            {
                _netHealth.Value = 0;
                health = 0;
                gameObject.GetComponentInParent<RigidBodyMovementScript>().enabled = false;
            } else
            {
				gameObject.GetComponentInParent<RigidBodyMovementScript>().enabled = true;
			}
		}
		else
		{
            health = _netHealth.Value;
			healthBar.fillAmount = health / MAX_HEALTH;
		}
        
    }

    [ServerRpc]
    public void RequestFireServerRpc(int damage) {
        FireClientRpc(damage);
    }

    [ClientRpc]
    private void FireClientRpc(int damage) {
        if (!IsOwner) ModifyHP(damage);
    }



}
