using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    private bool collided;
    void OnCollisionEnter (Collision co)
    {
        if(co.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            if (co.gameObject.tag == "Enemy")
            {
            Destroy (co.gameObject);
            }
            Destroy (gameObject);
        }
    }
}
