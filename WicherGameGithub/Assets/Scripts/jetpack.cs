using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetpack : MonoBehaviour
{
    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioSource cutout;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        
}



    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
           
            rb.AddForce(0, 80, 0);
            
        }
        else
        {
            rb.AddForce(0, -50, 0);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            audioSource.Play();
            cutout.Stop();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            audioSource.Stop();
            cutout.Play();
        }
    }
}
