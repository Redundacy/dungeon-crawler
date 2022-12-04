using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
	private NetworkVariable<int> _netHealth = new(writePerm: NetworkVariableWritePermission.Owner);
	public int health = 10;

	private void Start()
	{
		if (IsOwner)
		{
			_netHealth.Value = health;
		}
	}

	private void Update()
	{
		if (IsOwner)
		{
			_netHealth.Value = health;
		}
		else
		{
			health = _netHealth.Value;
		}
		if(_netHealth.Value <= 0)
		{
			if (gameObject.tag == "Boss")
			{
				Destroy(gameObject);
			}
			gameObject.SetActive(false);
		}
	}

	public void TakeDamage(int damage)
    {
        health -= damage;
    }

	[ServerRpc]
	public void RequestFireServerRpc(int damage)
	{
		FireClientRpc(damage);
	}

	[ClientRpc]
	private void FireClientRpc(int damage)
	{
		if (!IsOwner) TakeDamage(damage);
	}
}
