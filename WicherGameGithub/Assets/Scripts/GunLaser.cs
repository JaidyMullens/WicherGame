using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GunLaser : MonoBehaviour
{

    private RaycastHit hit;
    private Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public float damage = 100f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    private Light gunLight;
    private LineRenderer gunLine;

    public Transform shootingPoint;

    private float timer;
    private float effectsDisplayTime = 0.2f;
    private float timeBetweenLasers = 0.1f; // 0.15f

    private AudioSource laserSound;

    public bool foundRaycast;

    LaserManager laserManager;

    private bool hasAmmo = false;
    #region Initilization
    private void Start()
    {
        fpsCam = GetComponentInParent<Camera>();

        laserManager = GetComponent<LaserManager>();
        laserSound = GetComponent<AudioSource>();

        gunLight = shootingPoint.GetComponent<Light>();
        gunLine = shootingPoint.GetComponent<LineRenderer>();

    }
    #endregion

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;


        // Update the first point of the line to shooting point
        gunLine.SetPosition(0, shootingPoint.position);


        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenLasers)
        {
            float camAngleX = fpsCam.transform.eulerAngles.x;

            // Clamp values to be able to shoot 
            if((camAngleX >= 0 && camAngleX <= 33) || (camAngleX >= 270 && camAngleX <= 360))
            {
                // Variable to check if we have ammo from the LaserManager
                hasAmmo = laserManager.checkAmmo();

                Shoot();
                laserManager.subtractAmmo();
            }

        }
        if (timer >= timeBetweenLasers * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot()
    {
        if (hasAmmo)
        {
            // Reset the shooting timer
            timer = 0f;

            // Enable the laser effects
            gunLight.enabled = true;
            gunLine.enabled = true;
            muzzleFlash.Play();
            laserSound.Play();

            // Get raycast from the Camera center to the crosshair
            foundRaycast = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);

            if (foundRaycast)
            {
                // Set positions of the line renderer 
                gunLine.SetPosition(0, shootingPoint.position);
                gunLine.SetPosition(1, hit.point);


                Target target = hit.transform.GetComponent<Target>();

                // Look for target to deal damage
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                // Look for rigidbody to add force when hit
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                
                // Instantiate an impact effect on the position of shooting
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.3f);

            }
            else
            {
                // Set end position of the laser to the end of our range point
                gunLine.SetPosition(1, shootingPoint.position + transform.forward * range);
            }
        }

    }
}
