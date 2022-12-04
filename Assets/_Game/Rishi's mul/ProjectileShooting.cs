using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ProjectileShooting : NetworkBehaviour
{
    public Camera cam;
    public GameObject projectile1;
    public Transform FirePoint;
    public float projectileSpeed = 30;

    private Vector3 destination;
    private float timeToFire;
    public float fireRate = 4;

    // Update is called once per frame
    void Update()
    {   
         // don't do anything if you're not the owner
        if (!IsOwner) return;

        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= timeToFire)
        {   
            var dir = transform.forward;
            // Send off the request to be executed on all clients
            RequestFireServerRpc();

            timeToFire = Time.time + 1/fireRate;
            ShootProjectile();
        }
    }

    [ServerRpc]
    private void RequestFireServerRpc() {
        FireClientRpc();
    }

    [ClientRpc]
    private void FireClientRpc() {
        if (!IsOwner) ShootProjectile();
    }


    void ShootProjectile()
    {
        InstantiateProjectile(FirePoint);
    }
      

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj1 = Instantiate (projectile1, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj1.GetComponent<Rigidbody>().velocity = (transform.forward).normalized * projectileSpeed;
    }

    // public override void OnNetworkSpawn() {
    //     if (!IsOwner) Destroy(this);
    // }
}
