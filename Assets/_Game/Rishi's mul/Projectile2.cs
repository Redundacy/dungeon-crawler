using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    private bool collided;
	void OnTriggerEnter(Collider co)
	{
		if (co.gameObject.tag == "Enemy" && !collided)
		{
			collided = true;
			co.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
			co.gameObject.GetComponent<EnemyHealth>().RequestFireServerRpc(1);
			Destroy(gameObject);
		} else if((co.gameObject.tag == "Player") && !collided) {
			co.gameObject.GetComponentInChildren<HealthBar>().ModifyHP(1);
			Destroy(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
