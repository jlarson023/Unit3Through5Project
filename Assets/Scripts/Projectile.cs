using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float forwardForce;
    public float upForce;
    public Rigidbody rb;
    public float destroyDelay;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * forwardForce, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.up * upForce, ForceMode.Impulse);

        Destroy(gameObject, destroyDelay);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            gameManager.UpdateScore(1);
        }
    }
}
