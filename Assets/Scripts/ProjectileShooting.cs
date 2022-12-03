using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
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
        if(Input.GetKey("e") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1/fireRate;
            ShootProjectile();
        }
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
        projectileObj1.GetComponent<Rigidbody>().velocity = (transform.forward).normalized * projectileSpeed;
        }
        else 
        {
        var projectileObj2 = Instantiate (projectile2, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj2.GetComponent<Rigidbody>().velocity = (transform.forward).normalized * projectileSpeed;
        }
    }
}
