using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Turning")]
    public float turnSpeed;
    public float horizontalInput;
    [Header ("Shooting")]
    public GameObject projectile;
    public GameObject bulletSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TURN
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        
        //SHOOT
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        }
    }
}
