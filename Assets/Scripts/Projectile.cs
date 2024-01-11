using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float forwardForce;
    public float upForce;
    public Rigidbody rb;
    public float destroyDelay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * forwardForce, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.up * upForce, ForceMode.Impulse);

        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
