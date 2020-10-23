using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunRaycast : MonoBehaviour
{
    public float damage = 100f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    private Camera fpsCam;
    private ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

   
    private void Start()
    {
        fpsCam = GetComponentInParent<Camera>();

        muzzleFlash = this.gameObject.transform.Find("ShootingPoint").transform.Find("Muzzle Flash").GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (Time.time >= nextTimeToFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
     
   
    }

    

    void Shoot()
    {


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            muzzleFlash.Play();

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.3f);

        }
        
    }

    
}
