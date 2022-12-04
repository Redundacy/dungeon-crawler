using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
	private bool collided;
	void OnTriggerEnter(Collider co)
	{
		if ((co.gameObject.tag == "Enemy" || co.gameObject.tag == "Boss") && !collided)
		{
			collided = true;
			co.gameObject.GetComponent<EnemyHealth>().TakeDamage(2);
			//co.gameObject.GetComponent<EnemyHealth>().RequestFireServerRpc(2);
			Destroy(gameObject);
		} else
		{
			Destroy(gameObject);
		}
	}
}
