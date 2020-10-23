using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GunPhysics : MonoBehaviour
{
    public float damage = 100f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public float bulletSpeed = 50f;

    private Camera fpsCam;
    private ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject projectile;

    private float nextTimeToFire = 0f;

    public Transform gunCursor;
    public Transform centerPointCursor3D;
    public Transform gunCursorMagic;

    private Transform shootingPoint;


    public float offset = 0.13f;

    public float scaleFactor;

    private void Start()
    {
        fpsCam = GetComponentInParent<Camera>();

        muzzleFlash = this.gameObject.transform.Find("ShootingPoint").transform.Find("Muzzle Flash").GetComponent<ParticleSystem>();

        shootingPoint = gameObject.transform.Find("ShootingPoint").transform;

        Vector3 direction = (centerPointCursor3D.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);


    }


    private void Update()
    {

        
        RaycastHit hitInfo;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hitInfo, float.MaxValue)) // 35.07286f
        {

            gunCursor.position = hitInfo.point;

        }

 

        //  gunCursor.localScale = (0.027941833086f * hitInfo.distance) * Vector3.one + 0.08f * Vector3.one;

        float distance = (gunCursor.position - fpsCam.transform.position).magnitude;

        gunCursor.localScale = distance * scaleFactor * Vector3.one;



    }

 

    void Shoot()
    {

            muzzleFlash.Play();
            GameObject newBullet = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = shootingPoint.forward * bulletSpeed;

    }


    private void FixedUpdate()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

   


}

