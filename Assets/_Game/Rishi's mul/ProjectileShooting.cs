using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ProjectileShooting : NetworkBehaviour
{
    public Camera cam;
    public GameObject projectile1;
    public GameObject projectile2;
    public Transform leftFirePoint, rightFirePoint;
    public float projectileSpeed = 30;

    private Vector3 destination;
    private bool leftSide;
    private float timeToFire;
    public float fireRate = 4;

    // Update is called once per frame
    void Update()
    {   
         // don't do anything if you're not the owner
        if (!IsOwner) return;

        if(Input.GetKey("e") && Time.time >= timeToFire)
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
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else 
        {
            destination = ray.GetPoint(1000);
        }
        
        if(leftSide)
        {
            leftSide = false;
            InstantiateProjectile(leftFirePoint);
        }
        else
        {
            leftSide = true;
            InstantiateProjectile(rightFirePoint);
        }
        
    }

    void InstantiateProjectile(Transform firePoint)
    {
        if (leftSide)
        {
        var projectileObj1 = Instantiate (projectile1, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj1.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        }
        else 
        {
        var projectileObj2 = Instantiate (projectile2, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj2.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        }
    }

    // public override void OnNetworkSpawn() {
    //     if (!IsOwner) Destroy(this);
    // }
}
